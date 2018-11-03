using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSearcherProject
{
    public enum FileType : int
    {
        TXT,
        PDF
    }

    //public struct FileInfo
    //{
    //    public string Name { get; private set; }    // File Name
    //    public string Path { get; private set; }    // File Path
    //    public FileType Ext { get; private set; }    // File Extention indicating the file type (txt, pdf)

    //    Ctor, initialize local properties
    //    public FileInfo(string path, string name, FileType ext)
    //    {
    //        this.Name = name;
    //        this.Path = path;
    //        this.Ext = ext;
    //    }
    //}
}
