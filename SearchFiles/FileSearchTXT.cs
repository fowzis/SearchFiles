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
        protected override void SearchSync(FileInfo fileInfo, string searchStr, IList<FileInfo> foundFileList)
        {
            try
            {
                CheckSearchString(searchStr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exiting. Invalid Parameters!.\r\n {0}", e.Message);
                throw e;
            }

            // Validate the file is still there
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(fileInfo.FullName);
            }

            try
            {
                // Open the file for reading in Text mode.
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    string line;

                    // Search for the required sensitive text
                    while ((line = sr.ReadLine()) != null)
                    {

                        if (line.Contains(searchStr))
                        {
                            foundFileList.Add(fileInfo);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The Process failed: {0}", e.ToString());
                throw;    // propagate exception to caller;
            }
        }
    }
}
