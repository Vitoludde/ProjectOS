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
		public string[] commands = new string[] {"About - This", "Read (File) - Reads file direct to OS with filename", "List", "Write (File) (Content) - Writes a file with the file name and content", "Up - Go up a directory", "Go - Go into directory"};

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
				Sys.Power.Shutdown();
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
				Sys.Power.Shutdown();
			}

			Console.WriteLine("Starting Directory Handler");
			try
			{
				SystemPrograms.DirectoryHandler DirectoryHandler = new SystemPrograms.DirectoryHandler();
				Console.WriteLine("Started Directory Handler");
			}
			catch (InvalidCastException e)
			{
				Console.WriteLine(String.Format("FATAL ERROR! Cannot start Directory Handler. Error: {0}. Report this to the author immediately!", e));
				Sys.Power.Shutdown();
			}

			Console.WriteLine("Starting Auxiliary Commands");
			try
			{
				SystemPrograms.AuxiliaryCommands AuxiliaryCommands = new SystemPrograms.AuxiliaryCommands();
				Console.WriteLine("Started Auxiliary Commands");
			}
			catch (InvalidCastException e)
			{
				Console.WriteLine(String.Format("ERROR! Cannot start Auxiliary Commands. Error: {0}.", e));
			}

			AuxiliaryCommands.clear();
			Console.WriteLine("Boot Succesfull! Welcome to the OS!");
		}

		protected override void Run()
		{
			Console.Write(CurrentPath + ">");
			var input = Console.ReadLine();

			/*string[] inputs = input.Split(' ');

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
			}*/

			if (input == "about")
			{
				Console.WriteLine("Commands: ");
				foreach (string member in commands)
                {
					Console.WriteLine(member);
                }
			}

			if (input == "read")
			{
				Console.Write("Filename: ");
				var name = Console.ReadLine();

				FileHandler.ReadFile(name);
			} 
			
			else if (input == "write")
			{
				Console.Write("Filename: ");
				var name = Console.ReadLine();

				Console.Write("Content: ");
				var content = Console.ReadLine();

				FileHandler.WriteFile(name, content);
			} 
			
			else if (input == "list")
			{
				DirectoryHandler.GetFiles(CurrentPath);
			}

			else if (input == "go")
			{
				Console.Write("Directory Name: ");
				var name = Console.ReadLine();

				DirectoryHandler.GoTo(name);
			}

			else if (input == "up")
			{
				DirectoryHandler.DirUp();
			}

			else if (input == "quit")
            {
				AuxiliaryCommands.Shutdown();
			}

			else if (input == "shutdown")
			{
				AuxiliaryCommands.Shutdown();
			}
		}
	}
}
