using FileSearcher.Models;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;

namespace FileSearcher.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel()
        {
            FileReader fr = new FileReader();
            List<File> lf = new List<File>();
            List<File> cd = new List<File>();
            SelectFolderDialog sfd = new SelectFolderDialog();
            //string path=sfd.SelectFolder();
            fr.GetFilesToShow(@"F:\bookfilter",lf);
            //fr.GetFilesToShow(path, lf);
            fr.GetFilesToShowInCurrentDir(@"F:\bookfilter\www", cd);
            _filesToShow = new ObservableCollection<File>(lf);
            _filesToShowInCurrentDir = new ObservableCollection<File>(cd);
        }
        private ObservableCollection<File> _filesToShow;
        public ObservableCollection<File> FilesToShow
        {
            get { return _filesToShow; }
            set
            {
                _filesToShow = value;
                OnPropertyChanged(nameof(_filesToShow));
            }
        }

        private ObservableCollection<File> _filesToShowInCurrentDir;
        public ObservableCollection<File> FilesToShowInCurrentDir
        {
            get { return _filesToShowInCurrentDir; }
            set
            {
                _filesToShowInCurrentDir = value;
                OnPropertyChanged(nameof(_filesToShowInCurrentDir));
            }
        }
    }
}
