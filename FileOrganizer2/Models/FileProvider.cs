using System.Collections.Generic;

namespace FileOrganizer2.Models
{
    public class FileProvider : IFileProvider
    {
        public void LoadFiles(string path)
        {
            System.Diagnostics.Debug.WriteLine($"{path}(FileProvider : 10)");
        }

        public IEnumerable<ExtendFileInfo> GetExtendFileInfos()
        {
            return new List<ExtendFileInfo>();
        }
    }
}