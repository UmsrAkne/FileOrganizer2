using System.Diagnostics;
using Prism.Mvvm;

namespace FileOrganizer2.Models
{
    public class TextWrapper : BindableBase
    {
        private string title;
        private string version = string.Empty;

        public TextWrapper()
        {
            Title = "FileOrganizer2";

            SetVersion();
            AddDebugMark();
        }

        public string Title
        {
            get => string.IsNullOrWhiteSpace(Version)
                ? title
                : title + " version : " + Version;
            private set => SetProperty(ref title, value);
        }

        private string Version { get => version; set => SetProperty(ref version, value); }

        public override string ToString()
        {
            return Title;
        }

        [Conditional("RELEASE")]
        private void SetVersion()
        {
            Version = "20240809" + "a";
        }

        [Conditional("DEBUG")]
        private void AddDebugMark()
        {
            Title += " (Debug)";
        }
    }
}