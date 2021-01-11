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
                Console.WriteLine(String.Format("FATAL ERROR! Cannot Initialize FAT File System. Error: {0}", e));
            }

            Console.WriteLine("Starting File Handler");
            try
            {
                SystemPrograms.FileHandler FileHandler = new SystemPrograms.FileHandler();
                Console.WriteLine("Started File Handler");
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(String.Format("FATAL ERROR! Could not start File Handler. Error: {0}", e));
            }
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);

            
        }
    }
}
