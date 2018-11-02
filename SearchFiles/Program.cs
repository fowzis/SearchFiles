using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string fullFilePath = "empty";
            string searchString = "empty";

            if (args.Length > 1)
            {
                fullFilePath = args[0];
                searchString = args[1];
            }

            Console.WriteLine("File: {0}", fullFilePath);
            Console.WriteLine("Search String: {0}", searchString);
            Console.WriteLine();

            IList<int> results = null;

            try
            {
                FileSearchBase fs = FileSearchFactory.GetSearchAgent(FileType.TXT);
                fs.Search(fullFilePath, searchString, out results);

                foreach (var item in results)
                {
                    Console.WriteLine("Row: {0}", item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }
    }
}
