using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

using ProjectOS;

namespace ProjectOS.SystemPrograms
{
    class FileHandler
    {
        static ProjectOS.Kernel Kernel = new ProjectOS.Kernel();

        public static void ReadFile(string Filename)
        {
            try
            {
                var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.CurrentPath + Filename);
                var hello_file_stream = hello_file.GetFileStream();

                if (hello_file_stream.CanRead)
                {
                    byte[] text_to_read = new byte[hello_file_stream.Length];
                    hello_file_stream.Read(text_to_read, 0, (int)hello_file_stream.Length);
                    Console.WriteLine(Encoding.Default.GetString(text_to_read));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void WriteFile(string Filename, string Content)
        {
            try
            {
                var file = Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.CurrentPath + "\\" + Filename);
                var file_stream = file.GetFileStream();

                if (file_stream.CanWrite)
                {
                    file_stream.Write(Encoding.ASCII.GetBytes(Content), 0, Content.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
