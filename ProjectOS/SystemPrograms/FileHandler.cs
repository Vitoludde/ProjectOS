using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace ProjectOS.SystemPrograms
{
    class FileHandler
    {
        void ReadFile(string Path)
        {
            try
            {
                var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(Path);
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

        void WriteFile(string PathToFile, string Filename, string Content)
        {
            try
            {
                var file = Sys.FileSystem.VFS.VFSManager.GetFile(PathToFile + "\\" + Filename);
                if (file != null)
                {
                    var file_stream = file.GetFileStream();

                    if (file_stream.CanWrite)
                    {
                        file_stream.Write(Encoding.ASCII.GetBytes(Content), 0, Content.Length);
                    }
                } else
                {
                    var newfile = Sys.FileSystem.VFS.VFSManager.CreateFile(PathToFile + "\\" + Filename);
                    var newfile_stream = newfile.GetFileStream();

                    if (newfile_stream.CanWrite)
                    {
                        newfile_stream.Write(Encoding.ASCII.GetBytes(Content), 0, Content.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        void GetFiles(string Path)
        {
            var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(Path);
            foreach (var directoryEntry in directory_list)
            {
                Console.WriteLine(directoryEntry.mName);
            }
        }
    }
}
