using System;
using System.Collections.Generic;
using System.Text;

using Sys = Cosmos.System;

namespace ProjectOS.SystemPrograms
{
    class DirectoryHandler
    {
        static ProjectOS.Kernel Kernel = new ProjectOS.Kernel();

        public static void GetFiles(string Path)
        {
            try
            {
                var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(Path);
                foreach (var directoryEntry in directory_list)
                {
                    Console.WriteLine(directoryEntry.mName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void GoTo(string DirName)
        {
            try
            {
                Kernel.CurrentPath = Kernel.CurrentPath + "\\" + DirName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void DirUp()
        {
            try
            {
                char[] separators = new char[] { '\\', '/' };

                String[] dirs = Kernel.CurrentPath.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                dirs[dirs.Length - 1] = "";

                Kernel.CurrentPath = string.Join("\\", dirs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
