using System.Collections.Generic;

namespace FileOrganizer2.Models
{
    public class DummyFileProvider : IFileProvider
    {
        private readonly List<ExtendFileInfo> files;

        public DummyFileProvider()
        {
            files = new List<ExtendFileInfo>();
            for (var i = 0; i < 30; i++)
            {
                files.Add(new ExtendFileInfo($"test/testFile_{i:0000}")
                {
                    Ignore = false,
                    Index = 0,
                    TentativeName = string.Empty,
                });
            }
        }

        public IEnumerable<ExtendFileInfo> GetExtendFileInfos()
        {
            return files;
        }
    }
}