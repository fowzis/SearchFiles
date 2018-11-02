using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    static class FileSearchFactory
    {
        public static FileSearchBase GetSearchAgent(FileType fileType)
        {
            FileSearchBase FileSearchAgent = null;

            switch (fileType)
            {
                case FileType.TXT:
                    FileSearchAgent = new FileSearchTXT();
                    break;
                case FileType.PDF:
                    FileSearchAgent = new FileSearchPDF();
                    break;
                default:
                    FileSearchAgent = null;
                    throw new Exception("File type '" + fileType.ToString() + "' is not supported");
            }

            return FileSearchAgent;
        }
    }
}
