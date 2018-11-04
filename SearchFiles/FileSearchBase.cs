using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    public class FileSearchBase
    {
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

        public virtual IList<FileInfo> SearchSync(FileInfo fileInfo, string searchStr)
        {
            List<FileInfo> foundFileList = new List<FileInfo>();

            SearchSync(fileInfo, searchStr, foundFileList);

            return foundFileList;
        }

        protected virtual void SearchSync(FileInfo fileInfo, string searchStr, IList<FileInfo> foundFileList)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
