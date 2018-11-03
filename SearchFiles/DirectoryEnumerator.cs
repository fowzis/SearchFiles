using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace FilesSearcherProject
{
    class DirectoryTree
    {
        Queue<DirectoryInfo> dirsQueue = new Queue<DirectoryInfo>();

        DirectoryTree(string folderName)
        {
            CheckInitialFolder(folderName);
        }

        // Check if initial folder exists
        private void CheckInitialFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
                throw new DirectoryNotFoundException();
        }

        // Recursivly populate the ConcurrentQueue collection.
        //public static void GetDirectories(string startFolder)
        //{
        //    List<DirectoryInfo> dirTreeContent = new List<DirectoryInfo>();

        //    GetDirectories(startFolder, dirTreeContent);

        //    return dirTreeContent;
        //}

        /// <summary>
        /// get all directory under a specified existing directory
        /// Assumption is that the initial directory already exists
        /// </summary>
        /// <param name="startFolder"></param>
        /// <returns></returns>
        private void GetDirList(string startFolder, List<DirectoryInfo> results)
        {
            DirectoryInfo[] dirInfoArr = null;
            DirectoryInfo di = null;

            try
            {
                // Get Reference to the Directory
                di = new DirectoryInfo(startFolder);

                // Get array od subdirectories
                dirInfoArr = di.GetDirectories();

                // If empty, return
                if (dirInfoArr.Length == 0)
                    return;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex);
                return;
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine(ex);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            foreach (var dir in dirInfoArr)
            {
                // Recursevely check all subfolders
                GetDirList(dir.Name, results);
            }

            // Reach here once all subdirectories have been visited
            //foreach (var item in di.GetFiles)
            //{

            //}
        }
    }
}
