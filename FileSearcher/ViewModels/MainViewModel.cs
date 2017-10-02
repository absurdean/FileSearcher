using FileSearcher.Models;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System;
using PluginCommon;
using System.Windows;
using System.Threading.Tasks;

namespace FileSearcher.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel(Window mainUI,SelectFolderDialog selectFolderObj,PluginHost pluginHost)
        {   
            _upperSize = 99999999;
            _filterDate = DateTime.Now;
            _containingWord = "";
            _pluginNames = new ObservableCollection<string>();
            _pluginIndex = 0;
            _selectFolderObj = selectFolderObj;
            _pluginControls = new ObservableCollection<PluginCommon.IView>();
            _pluginNames = new ObservableCollection<string>();


            pluginHost.RefreshPlugins(PluginNames, PluginControls, _methods);
            _isSearchEnabled = true;

            _mainUI = mainUI;

            if (PluginControls.Count!=0)
            {
                PluginView = PluginControls[0];
                pluginHost.ChangePlugins(PluginControls,ref _pluginIndex);
            }
            _filesToSearching = new List<FileToSearch>();
            _filesToSearchingInCurrentDir=new List<FileToSearch>();

            FilesToShow = new ObservableCollection<FileToSearch>(_filesToSearching);


            _searchFilesCommand = new DelegateCommand(() =>
            {
                Search();
            }, SearchCommand_CanExecute);

            _changePluginCommand = new DelegateCommand(() =>
            {
                pluginHost.ChangePlugins(PluginControls,ref _pluginIndex);
            });
            _refreshPluginCommand = new DelegateCommand(() =>
            {
                pluginHost.RefreshPlugins(PluginNames, PluginControls, _methods);
            });
            _stopSearchCommand = new DelegateCommand(() =>
            {
                IsStopSearch = true;
                MessageBox.Show("Searching was stopped!","",MessageBoxButton.OK,MessageBoxImage.Information);
                IsStopSearch = false;
            });

            _changeFolderCommand = new DelegateCommand(() =>
            {
                _selectFolderObj.ChangeFolder(_filesToSearching,_filesToSearchingInCurrentDir,FilesToShow);
            });
        }


        private Window _mainUI;
        public bool AddFileAndRefresh(FileToSearch file)
        {
            _mainUI.Dispatcher.Invoke(new Action(() =>
            {
                FilesToShow.Add(file);
            }));
            if (_isStopSearch)
            { return true; }
            else { return false; }
        }

        private void Search()
        {
            if (PluginView != null)
            {
                if ((_filesToSearching.Count != 0) && (_filesToSearching != null))
                {
                    _isSearchEnabled = false;
                    FilesToShow.Clear();
                    if (_isSubfolders)
                    {
                        Task.Run(() =>
                        {
                            _isSearchEnabled = _methods[_pluginIndex].SearchFiles(AddFileAndRefresh, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                        });
                    }
                    else
                    {
                        Task.Run(() =>
                        {
                            _isSearchEnabled = _methods[_pluginIndex].SearchFiles(AddFileAndRefresh, _filesToSearchingInCurrentDir, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                        });
                    }
                }
                else
                {
                    MessageBox.Show("List of files is empty", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Any plugins was found", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private SelectFolderDialog _selectFolderObj;
        private List<FileToSearch> _filesToSearching;
        private List<FileToSearch> _filesToSearchingInCurrentDir;

        private PluginCommon.IView PluginViewVar;
        public PluginCommon.IView PluginView
        {
            get { return PluginViewVar; }
            set
            {
                PluginViewVar = value;
                OnPropertyChanged("PluginView");
            }
        }



        private ObservableCollection<PluginCommon.IView> _pluginControls;
        public ObservableCollection<PluginCommon.IView> PluginControls
        {
            get { return _pluginControls; }
            set
            {
                _pluginControls = value;
                OnPropertyChanged(nameof(_pluginControls));
            }
        }

        private ObservableCollection<string> _pluginNames;
        public ObservableCollection<string> PluginNames
        {
            get { return _pluginNames; }
            set
            {
                _pluginNames = value;
                OnPropertyChanged(nameof(_pluginNames));
            }
        }


        private List<IPlugin> _methods = new List<IPlugin>();

        private int _pluginIndex;

        private bool _isStopSearch;
        public bool IsStopSearch
        {
            get { return _isStopSearch; }
            set
            {
                _isStopSearch = value;
                OnPropertyChanged(nameof(_isStopSearch));
            }
        }


        private ObservableCollection<FileToSearch> _filesToShow;
        public ObservableCollection<FileToSearch> FilesToShow
        {
            get {
                return _filesToShow; }
            set
            {
                _filesToShow = value;
                OnPropertyChanged(nameof(_filesToShow));
            }
        }

        private bool _isSubfolders;
        public bool IsSubfolders
        {
            get { return _isSubfolders; }
            set
            {
                _isSubfolders = value;
                OnPropertyChanged(nameof(_isSubfolders));
            }
        }

        private bool _isSearchEnabled;

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

        private ICommand _searchFilesCommand;
        public ICommand SearchFilesCommand
        {
            get
            {
                return _searchFilesCommand;
            }
        }



        public bool SearchCommand_CanExecute()
        {
            return _isSearchEnabled;
        }


        private ICommand _stopSearchCommand;
        public ICommand StopSearchCommand
        {
            get
            {
                return _stopSearchCommand;
            }
        }

        private ICommand _changePluginCommand;
        public ICommand ChangePluginCommand
        {
            get
            {
                return _changePluginCommand;
            }
        }

        private ICommand _refreshPluginCommand;
        public ICommand RefreshPluginCommand
        {
            get
            {
                return _refreshPluginCommand;
            }
        }

        private ICommand _changeFolderCommand;
        public ICommand ChangeFolderCommand
        {
            get
            {
                return _changeFolderCommand;
            }
        }
    }
}
