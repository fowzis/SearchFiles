using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length > 1)
            //{
            //    string fullFilePath = args[0];
            //    string searchString = args[1];

            //    TEST_SearchInFileText(fullFilePath, searchString);
            //}
            //else if (args.Length == 1)
            //{
            //    string startPath = args[0];

            //    TEST_GetDirectoryTree(startPath);
            //}

            if (args.Length > 1)
            {
                string fullFilePath = args[0];
                string searchString = args[1];
                TEST_SearchFilesInDirTree(fullFilePath, searchString);
            }

            Console.ReadLine();
        }

        private static void TEST_SearchFilesInDirTree(string startPath, string searchString)
        {
            List<FileInfo> fileInfoList = null;
            List<FileInfo> fileHitList = new List<FileInfo>();
            IList<FileInfo> results = null;

            Console.WriteLine("Start Path: {0}", startPath);

            DirectoryTree dirTree = new DirectoryTree(startPath);
            fileInfoList = dirTree.GetFilesInDirTreeSync(startPath);

            foreach (FileInfo fileInfo in fileInfoList)
            {
                try
                {
                    string fileExt = fileInfo.Extension.Trim('.').ToUpper();

                    FileType fileType;
                    if (Enum.TryParse<FileType>(fileExt, out fileType))
                    {
                        FileSearchBase fs = FileSearchFactory.GetSearchAgent(fileType);

                        results = fs.SearchSync(fileInfo, searchString);
                        fileHitList.AddRange(results);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            if (fileHitList.Count > 0)
            {
                foreach (var item in fileHitList)
                {
                    Console.WriteLine("Row: {0}", item);
                }
            }
        }

        private static void TEST_GetFilesInDirTree(string startPath)
        {
            List<FileInfo> dirInfoList = null;

            Console.WriteLine("Start Path: {0}", startPath);
            
            DirectoryTree dt = new DirectoryTree(startPath);
            dirInfoList = dt.GetFilesInDirTreeSync(startPath);

            foreach (var item in dirInfoList)
            {
                Console.WriteLine("{0}", item.FullName);
            }
        }

        private static void TEST_SearchFile(string fullFilePath, string searchString)
        {
            Console.WriteLine("File: {0}", fullFilePath);
            Console.WriteLine("Search String: {0}", searchString);
            Console.WriteLine();

            IList<FileInfo> results = null;

            try
            {
                FileSearchBase fs = FileSearchFactory.GetSearchAgent(FileType.PDF);

                results = fs.SearchSync(new FileInfo(fullFilePath), searchString);
                if (results.Count > 0)
                {
                    foreach (var item in results)
                    {
                        Console.WriteLine("Row: {0}", item);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
