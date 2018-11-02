using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    interface IFileSearch
    {
        int Search(string fileNamePath, string searchStr, out IList<int> searchResults);
    }

    public class FileSearchBase : IFileSearch
    {
        #region Private Members

        protected List<int> foundRowsList = null;

        #endregion

        #region Class Properties
        public string FileName { get; private set; }
        public string FileExt { get; private set; }
        public string FilePath { get; private set; }

        // A property that returns a readonly reference to the list of rows per file, where the search string was found.
        public ref readonly List<int> GetRows { get { return ref foundRowsList; } }

        /// <summary>
        /// At first found occurance, instanciate the List holding the line found numbers.
        /// </summary>
        /// <param name="row"></param>
        public void AddFound(int row)
        {
            if (foundRowsList == null)
            {
                foundRowsList = new List<int>();
            }

            foundRowsList.Add(row);
        }
        #endregion

        // Constructor
        public FileSearchBase() { }

        #region Private Methods

        protected void CheckSearchString(string sensitiveText)
        {
            if (sensitiveText == null)
                throw new ArgumentNullException(nameof(sensitiveText), "Argumaent is null.");

            if (sensitiveText == String.Empty)
                throw new ArgumentException("Invalid Argument: ", nameof(sensitiveText));
        }

        protected void CheckFile(string fileNamePath)
        {
            if (fileNamePath == null)
                throw new ArgumentNullException(nameof(fileNamePath), "Argumaent is null.");

            if (fileNamePath == String.Empty)
                throw new ArgumentException("Invalid Argument: ", nameof(fileNamePath));

            FileName = Path.GetFileName(fileNamePath);
            FileExt = Path.GetExtension(fileNamePath);
            FilePath = Path.GetDirectoryName(fileNamePath);

            Console.WriteLine("File Name: {0}", FileName);
            Console.WriteLine("File Ext : {0}", FileExt);
            Console.WriteLine("File Path: {0}", FilePath);
        }

        public virtual int Search(string fileNamePath, string searchStr, out IList<int> searchResults)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
