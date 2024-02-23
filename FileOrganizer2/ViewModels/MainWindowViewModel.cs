using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FileOrganizer2.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace FileOrganizer2.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private readonly Renamer fileNameChanger = new ();
        private double listViewItemLineHeight = 15;
        private double fontSize = 12.0;

        public string Title => "File Organizer2";

        public double ListViewItemLineHeight
        {
            get => listViewItemLineHeight;
            private set => SetProperty(ref listViewItemLineHeight, value);
        }

        public double FontSize { get => fontSize; private set => SetProperty(ref fontSize, value); }

        public DelegateCommand<object> SetFontSizeCommand => new ((size) =>
        {
            FontSize = (double)size;
            ListViewItemLineHeight = (int)((double)size * 1.25);
        });

        public FileContainer FileContainer { get; } = new (new List<ExtendFileInfo>());

        public DelegateCommand<object> PageDownCommand => new ((lvActualHeight) =>
        {
            FileContainer.MoveCursorCommand.Execute(GetDisplayingItemCount((double)lvActualHeight));
        });

        public DelegateCommand<object> PageUpCommand => new ((lvActualHeight) =>
        {
            FileContainer.MoveCursorCommand.Execute(GetDisplayingItemCount((double)lvActualHeight) * -1);
        });

        public DelegateCommand PlaySoundCommand => new (() =>
        {
            if (FileContainer.SelectedItem == null)
            {
                return;
            }

            if (FileContainer.SelectedItem.IsSoundFile)
            {
                FileContainer.Files.ToList().ForEach(f => f.Playing = false);
                FileContainer.SelectedItem.Playing = true;

                // windowsMediaPlayer.URL = FileContainer.SelectedItem.FileInfo.FullName;
                // windowsMediaPlayer.controls.play();
                return;
            }

            Process.Start(FileContainer.SelectedItem.FileInfo.FullName);
        });

        public DelegateCommand AppendPrefixToIgnoreFilesCommand => new (() =>
        {
            var ignoreFiles = FileContainer.Files.Where(f => f.Ignore);
            fileNameChanger.AppendPrefix("ignore_", ignoreFiles);
        });

        public DelegateCommand AppendNumberCommand => new (() =>
        {
            fileNameChanger.AppendNumber(FileContainer.Files);
        });

        public DelegateCommand AppendNumberWithoutIgnoreFileCommand => new (() =>
        {
            var files = FileContainer.Files.Where(f => !f.Ignore);
            fileNameChanger.AppendNumber(files);
        });

        private int GetDisplayingItemCount(double lvActualHeight)
        {
            // + 5 はボーダー等によるズレの補正値。厳密に正確な表示数が出るわけではない。大体当たっている程度。
            return (int)Math.Floor(lvActualHeight / (ListViewItemLineHeight + 5));
        }
    }
}