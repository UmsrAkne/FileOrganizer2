using System.Collections.Generic;

namespace FileOrganizer2.Models
{
    public interface IFileProvider
    {
        IEnumerable<ExtendFileInfo> GetExtendFileInfos();
    }
}