using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

using ProjectOS.SystemPrograms;

namespace ProjectOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");

            Console.WriteLine("Initializing FAT File System");
            try
            {
                CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                Console.WriteLine("Initialized FAT File System");
            } catch (InvalidCastException e)
            {
                Console.WriteLine(String.Format("FATAL ERROR! Cannot Initialize FAT File System. Error: {0}. Report this to the author immediately!", e));
                Environment.Exit(0);
            }

            Console.WriteLine("Starting File Handler");
            try
            {
                SystemPrograms.FileHandler FileHandler = new SystemPrograms.FileHandler();
                Console.WriteLine("Started File Handler");
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(String.Format("FATAL ERROR! Cannot start File Handler. Error: {0}. Report this to the author immediately!", e));
                Environment.Exit(0);
            }
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();

            string[] inputs = input.Split(' ');

            string[] args = new string[] {""};

            string cmd = inputs[0];

            int i = 0;

            foreach (string arg in inputs)
            {
                if (arg != cmd)
                {
                    args[i] = arg;
                }

                i++;
            }

            if (input == "about")
            {
                Console.WriteLine("File path is 0:/[PATH]");
            }

            if (input == "read")
            {
                if (args.Length > 0) 
                {
                    FileHandler.ReadFile(args[0]);
                } else
                {
                    Console.WriteLine("You need to specify a path!");
                }
            } else if (input == "write")
            {
                if (args.Length > 0)
                {
                    if (args.Length > 1)
                    {
                        if (args.Length > 2)
                        {
                            FileHandler.WriteFile(args[0], args[1], args[2]);
                        }
                        else
                        {
                            Console.WriteLine("You need to specify content!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You need to specify a filename!");
                    }
                }
                else
                {
                    Console.WriteLine("You need to specify a path!");
                }
            } else if (input == "list")
            {
                if (args.Length > 0)
                {
                    FileHandler.GetFiles(args[0]);
                }
                else
                {
                    Console.WriteLine("You need to specify a path!");
                }
            }


        }
    }
}
