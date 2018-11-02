using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    public class FileSearchTXT : FileSearchBase
    {
        public override int Search(string fileNamePath, string searchStr, out IList<int> searchResults)
        {
            try
            {
                CheckFile(fileNamePath);
                CheckSearchString(searchStr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exiting. Invalid Parameters!.\r\n {0}", e.Message);
                throw e;
            }

            // Validate the file is still there
            if (!File.Exists(fileNamePath))
            {
                throw new FileNotFoundException(fileNamePath);
            }

            try
            {
                // Open the file for reading in Text mode.
                using (StreamReader sr = new StreamReader(fileNamePath))
                {
                    string line;
                    int lineNum = 0;

                    // Search for the required sensitive text
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNum++;

                        if (line.Contains(searchStr))
                        {
                            this.AddFound(lineNum);
                            Console.WriteLine("Found {0} at line {1}", searchStr, lineNum.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The Process failed: {0}", e.ToString());
                throw e;    // propagate exception to caller;
            }

            searchResults = this.GetRows;
            return searchResults.Count;
        }
    }
}
