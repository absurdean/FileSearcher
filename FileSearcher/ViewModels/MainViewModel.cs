using FileSearcher.Models;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System;

namespace FileSearcher.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel(SearchManager searchManager)
        {
            _searchManager = searchManager;
            _upperSize = 20;
            _filterDate = DateTime.Now;
            _containingWord = "";
            FileReader fr = new FileReader();
           _filesToSearching = new List<File>();
            _filesToSearchingInCurrentDir=new List<File>();
            List<File> cd = new List<File>();
            SelectFolderDialog sfd = new SelectFolderDialog();
            //string path=sfd.SelectFolder();
            fr.GetFilesToShow(@"F:\bookfilter",_filesToSearching);
            //fr.GetFilesToShow(path, lf);
            fr.GetFilesToShowInCurrentDir(@"F:\bookfilter\www", _filesToSearchingInCurrentDir);
            _filesToShow = new ObservableCollection<File>(_filesToSearching);
            _filesToShowInCurrentDir = new ObservableCollection<File>(cd);

            _searchFilesCommand = new DelegateCommand(() =>
            {
                // if (_isCurrentAccount == true)
                // {
                //     NewComputerLogin = null;
                if (_isCurrentFolder)
                {
                    searchManager.SearchFiles(FilesToShow, _filesToSearchingInCurrentDir, _containingWord, LowerSize, UpperSize, FilterDate);
                }
                else
                {
                    searchManager.SearchFiles(FilesToShow, _filesToSearching, _containingWord, LowerSize, UpperSize, FilterDate);
                }
            });
        }
        private SearchManager _searchManager;
        private List<File> _filesToSearching;
        private List<File> _filesToSearchingInCurrentDir;
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

        private bool _isCurrentFolder;
        public bool IsCurrentFolder
        {
            get { return _isCurrentFolder; }
            set
            {
                _isCurrentFolder = value;
                OnPropertyChanged(nameof(_isCurrentFolder));
            }
        }

        private string _containingWord;
        public string ContainingWord
        {
            get { return _containingWord; }
            set
            {
                _containingWord = value;
                OnPropertyChanged(nameof(_containingWord));
            }
        }

        private double _lowerSize;
        public double LowerSize
        {
            get { return _lowerSize; }
            set
            {
                _lowerSize = value;
                OnPropertyChanged(nameof(_lowerSize));
            }
        }

        private double _upperSize;
        public double UpperSize
        {
            get { return _upperSize; }
            set
            {
                _upperSize = value;
                OnPropertyChanged(nameof(_upperSize));
            }
        }

        private DateTime _filterDate;
        public DateTime FilterDate
        {
            get { return _filterDate; }
            set
            {
                _filterDate = value;
                OnPropertyChanged(nameof(_filterDate));
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

        private ICommand _searchFilesCommand;
        public ICommand SearchFilesCommand
        {
            get
            {
                return _searchFilesCommand;
            }
        }
    }
}
