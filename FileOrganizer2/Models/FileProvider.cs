using System.Collections.Generic;

namespace FileOrganizer2.Models
{
    public class FileProvider : IFileProvider
    {
        public IEnumerable<ExtendFileInfo> GetExtendFileInfos()
        {
            return new List<ExtendFileInfo>();
        }
    }
}