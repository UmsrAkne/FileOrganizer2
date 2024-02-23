using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;

namespace FileOrganizer2.Models
{
    public class FileContainer : BindableBase
    {
        private List<ExtendFileInfo> files;
        private int cursorIndex = -1;
        private bool containsIgnoreFiles = true;
        private bool isReverseOrder;
        private int startIndex = 1;
        private ExtendFileInfo selectedItem;

        public FileContainer(IEnumerable<ExtendFileInfo> extendFileInfos)
        {
            OriginalFiles = extendFileInfos.ToList();
            ReloadCommand.Execute();

            if (Files.Count > 0)
            {
                CursorIndex = 0;
            }
        }

        public List<ExtendFileInfo> Files { get => files; private set => SetProperty(ref files, value); }

        public bool ContainsIgnoreFiles
        {
            get => containsIgnoreFiles;
            set
            {
                if (SetProperty(ref containsIgnoreFiles, value))
                {
                    ReloadCommand.Execute();
                }
            }
        }

        public bool IsReverseOrder
        {
            get => isReverseOrder;
            set
            {
                if (SetProperty(ref isReverseOrder, value))
                {
                    ReloadCommand.Execute();
                }
            }
        }

        public int StartIndex
        {
            get => startIndex;
            set
            {
                if (SetProperty(ref startIndex, value))
                {
                    ReloadCommand.Execute();
                }
            }
        }

        public int CursorIndex
        {
            get => cursorIndex;
            set
            {
                if (Files == null || Files.Count == 0)
                {
                    SetProperty(ref cursorIndex, -1);
                    return;
                }

                if (value < 0)
                {
                    SetProperty(ref cursorIndex, 0);
                    return;
                }

                if (value >= Files.Count)
                {
                    SetProperty(ref cursorIndex, Files.Count - 1);
                    return;
                }

                SetProperty(ref cursorIndex, value);
            }
        }

        public int IgnoreFileCount { private get; set; }

        public int MarkedFileCount { private get; set; }

        public DelegateCommand<object> MoveCursorCommand => new DelegateCommand<object>((count) =>
        {
            CursorIndex += (int)count;
        });

        public DelegateCommand JumpToNextMarkedFileCommand => new DelegateCommand(() =>
        {
            var nextMark = Files.Skip(CursorIndex + 1).FirstOrDefault(f => f.Marked);

            if (nextMark != null)
            {
                CursorIndex = Files.IndexOf(nextMark);
            }
        });

        public DelegateCommand JumpToPrevMarkedFileCommand => new DelegateCommand(() =>
        {
            var prevMark = Files.Take(CursorIndex).Reverse().FirstOrDefault(f => f.Marked);

            if (prevMark != null)
            {
                CursorIndex = Files.IndexOf(prevMark);
            }
        });

        public DelegateCommand ReloadCommand => new DelegateCommand(() =>
        {
            Files = OriginalFiles
                .Where(f => ContainsIgnoreFiles || !f.Ignore)
                .OrderBy(f => f.Name).ToList();

            if (IsReverseOrder)
            {
                Files.Reverse();
            }

            var index = StartIndex;

            foreach (var f in Files)
            {
                f.Index = !f.Ignore ? index++ : 0;
            }

            IgnoreFileCount = files.Count(f => f.Ignore);
            RaisePropertyChanged(nameof(IgnoreFileCount));

            MarkedFileCount = files.Count(f => f.Marked);
            RaisePropertyChanged(nameof(MarkedFileCount));

            MaximumIndex = Files.Max(f => f.Index);
            RaisePropertyChanged(nameof(MaximumIndex));

            CursorIndex = CursorIndex;
        });

        public ExtendFileInfo SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

        public DelegateCommand<ExtendFileInfo> ToggleIgnoreCommand => new DelegateCommand<ExtendFileInfo>((f) =>
        {
            f.Ignore = !f.Ignore;
            ReloadCommand.Execute();
        });

        public DelegateCommand<ExtendFileInfo> ToggleMarkCommand => new DelegateCommand<ExtendFileInfo>((f) =>
        {
            f.Marked = !f.Marked;
            ReloadCommand.Execute();
        });

        public object MaximumIndex { get; set; }

        private List<ExtendFileInfo> OriginalFiles { get; set; }
    }
}