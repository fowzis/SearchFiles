using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace FilesSearcherProject
{
    public class DirectoryTree
    {
        Queue<DirectoryInfo> dirsQueue = new Queue<DirectoryInfo>();

        public DirectoryTree(string folderName)
        {
            CheckInitialFolder(folderName);
        }

        // Check if initial folder exists
        private void CheckInitialFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
                throw new DirectoryNotFoundException();
        }

        // Synchronious
        // Recursivly populate the ConcurrentQueue collection.
        public List<FileInfo> GetFilesInDirTreeSync(string startFolder)
        {
            List<FileInfo> dirResult = new List<FileInfo>();

            GetDirsSync(startFolder, dirResult);

            return dirResult;
        }

        /// <summary>
        /// get all directory under a specified existing directory - Synchronious
        /// Assumption is that the initial directory already exists
        /// </summary>
        /// <param name="startFolder"></param>
        /// <returns></returns>
        private void GetDirsSync(string startFolder, List<FileInfo> foundFiles)
        {
            DirectoryInfo[] dirInfoArr = null;
            DirectoryInfo dirInfo = null;

            try
            {
                // Get Reference to the Directory
                dirInfo = new DirectoryInfo(startFolder);

                // Get array od subdirectories
                dirInfoArr = dirInfo.GetDirectories();

                // If empty, return
                if (dirInfoArr.Length == 0)
                    return;
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
            catch (PathTooLongException)
            {
                return;
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }

            foreach (var dir in dirInfoArr)
            {
                // Recursevely check all subfolders
                GetDirsSync(dir.FullName, foundFiles);
            }

            // Reach here once all subdirectories have been visited
            // Once at a leaf directory node, enumerate contained files and add them to the result list

            // Here dirInfo points to the current directory node
            // Obtain the string names of all the elements within myEnum 
            String[] fileExt = Enum.GetNames(typeof(FileType));
            foreach (var ext in fileExt)
            {
                foundFiles.AddRange(dirInfo.EnumerateFiles("*."+ext));
                if (foundFiles.Count > 0)
                    continue;
            }
        }
    }
}
