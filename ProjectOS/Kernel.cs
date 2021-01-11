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
		public string CurrentPath = "0:/";

		protected override void BeforeRun()
		{
			Console.WriteLine("Boot Succesfull");

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

			Console.WriteLine("Starting Auxiliary Commands");
			try
			{
				SystemPrograms.AuxiliaryCommands AuxiliaryCommands = new SystemPrograms.AuxiliaryCommands();
				Console.WriteLine("Started Auxiliary Commands");
			}
			catch (InvalidCastException e)
			{
				Console.WriteLine(String.Format("FATAL ERROR! Cannot start Auxiliary Commands. Error: {0}. Report this to the author immediately!", e));
				Environment.Exit(0);
			}

			AuxiliaryCommands.clear();
			Console.WriteLine("Boot Succesfull! Welcome to the OS!");
		}

		protected override void Run()
		{
			Console.Write(CurrentPath + ">");
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
				FileHandler.ReadFile(args[0]);
			} else if (input == "write")
			{
				FileHandler.WriteFile(args[0], args[1], args[2]);
			} else if (input == "list")
			{
				FileHandler.GetFiles(CurrentPath);
			} else if (input == "quit")
            {

            }

			
		}
	}
}
