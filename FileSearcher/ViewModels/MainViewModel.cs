using FileSearcher.Models;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System;
using PluginCommon;
using System.Windows;

namespace FileSearcher.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel(Window owner,SelectFolderDialog selectFolderObj,PluginHost pluginHost)
        {   
            _upperSize = 999999;
            _filterDate = DateTime.Now;
            _containingWord = "";
            _pluginNames = new ObservableCollection<string>();
            _selectFolderObj = selectFolderObj;
            _pluginControls = new ObservableCollection<PluginCommon.IView>();
            _pluginNames = new ObservableCollection<string>();
            pluginHost.RefreshPlugins(PluginNames, PluginControls, _methods);

            if (PluginControls.Count!=0)
            {
                PluginView = PluginControls[0];
                pluginHost.ChangePlugins(PluginControls,_pluginIndex);
            }
            FileReader fr = new FileReader();
            _filesToSearching = new List<FileToSearch>();
            _filesToSearchingInCurrentDir=new List<FileToSearch>();

            //string path=sfd.SelectFolder();
            fr.GetFilesToShow(@"F:\music new", _filesToSearching);
            fr.GetFilesToShowInCurrentDir(@"F:\music new", _filesToSearchingInCurrentDir);
            _filesToShow = new ObservableCollection<FileToSearch>(_filesToSearching);

            _searchFilesCommand = new DelegateCommand(() =>
            {
                if (PluginView != null)
                {
                    if (_filesToSearching != null)
                    {
                        if (_isSubfolders)
                        {
                            //Action action = () => { _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch); };
                            //var Search = new Thread(unused=>_methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch));
                            //Thread workerThread = new Thread(unused=> _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch));
                            //workerThread.Start();
                            //while(!workerThread.IsAlive)
                            //{
                            //    Thread.Sleep(1000);
                            //    workerThread.Join();
                            //    MessageBox.Show("konchilosya");
                            //}
                            _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                            //methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                            // owner.Dispatcher.BeginInvoke(action);
                        }
                    }
                    else
                    {
                        _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearchingInCurrentDir, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                    }
                
                }
                else
                {
                    MessageBox.Show("Any plugins was found");
                }
            });

            _changePluginCommand = new DelegateCommand(() =>
            {
                pluginHost.ChangePlugins(PluginControls, _pluginIndex);
            });
            _refreshPluginCommand = new DelegateCommand(() =>
            {
                pluginHost.RefreshPlugins(PluginNames, PluginControls, _methods);
            });
            _stopSearchCommand = new DelegateCommand(() =>
            {
                IsStopSearch = true;
                MessageBox.Show("Searching is stopped");
            });

            _changeFolderCommand = new DelegateCommand(() =>
            {
                _searchingPath=_selectFolderObj.SelectFolder();
            });
        }

        //public Task meth()
        //{
        //    return TaskEx.Run(() =>
        //    {
        //        _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
        //    });
        //}

        //    public async void searchF()
        //{
        //   var asyncSearch=Task.Factory.StartNew(()=> _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch));
        //}

        private SelectFolderDialog _selectFolderObj;
        private List<FileToSearch> _filesToSearching;
        private List<FileToSearch> _filesToSearchingInCurrentDir;
        private string _searchingPath="";


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
            get { return _filesToShow; }
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
