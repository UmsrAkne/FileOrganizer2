using System.Collections.Generic;

namespace FileOrganizer2.Models
{
    public class DummyFileProvider : IFileProvider
    {
        private List<ExtendFileInfo> files = new ()
        {
        };

        public DummyFileProvider()
        {
            for (var i = 0; i < 30; i++)
            {
                files.Add(new ($"test/testFile_{i}")
                {
                    Ignore = false,
                    Index = 0,
                    TentativeName = string.Empty,
                });
            }
        }

        public void LoadFiles(string path)
        {
            System.Diagnostics.Debug.WriteLine($"{path} が LoadFiles に渡されました。(DummyFileProvider : 9)");
        }

        public IEnumerable<ExtendFileInfo> GetExtendFileInfos()
        {
            return files;
        }
    }
}