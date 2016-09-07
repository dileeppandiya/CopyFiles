using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopyFiles
{
    class Program
    {
        static void Main()
        {
            // Copy from the current directory, include subdirectories.
            DirectoryCopy(".", @".\temp", true);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            //FileInfo[] files = dir.GetFiles();
            FileInfo[] files = GetFileName(dir,destDirName);
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            //if (copySubDirs)
            //{
            //    foreach (DirectoryInfo subdir in dirs)
            //    {
            //        string temppath = Path.Combine(destDirName, subdir.Name);
            //        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            //    }
            //}
        }


        private static FileInfo[] GetFileName(DirectoryInfo Sourcedir, string destDirName)
        {

            FileInfo[] files = new DirectoryInfo(destDirName).GetFiles();

            DateTime dt =   File.GetLastWriteTimeUtc(destDirName);
           
            FileInfo[] Sourcefiles = Sourcedir.GetFiles();

            FileInfo[] t = Sourcefiles.Where(X => X.CreationTimeUtc > dt).ToArray();
//            FileInfo[] tfiles = null;

            //foreach (FileInfo i in Sourcefiles)
            //{

            //    if (i.CreationTimeUtc > dt)
            //    {
            //        Console.WriteLine(i.Name);
            //    }
            //}
           
            
            return t;
        }
    }
}
