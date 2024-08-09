using System.Collections.Generic;
using System.IO;

namespace FileOrganizer2.Models
{
    public interface IFileProvider
    {
        void LoadFiles(string path);

        IEnumerable<ExtendFileInfo> GetExtendFileInfos();
    }
}