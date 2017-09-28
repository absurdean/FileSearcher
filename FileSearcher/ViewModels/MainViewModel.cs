using FileSearcher.Models;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System;
using PluginCommon;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace FileSearcher.ViewModels
{
    public class MainViewModel:BindableBase
    {

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



        private List<PluginCommon.IView> _pluginControls;

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


        [ImportMany(typeof(PluginCommon.IView), AllowRecomposition = true)]
        public IEnumerable<Lazy<PluginCommon.IView,IPluginMetadata>> Plugins;

        [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
        public IEnumerable<Lazy<IPlugin>> SearchMethod;

        private AggregateCatalog _catalog;
        private CompositionContainer _container;
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
        //убрать при рефакторе
        private ComboBox _comboBoxPlugins;

        public MainViewModel(SelectFolderDialog selectFolderObj, ComboBox comboBoxPlugins, ContentControl contentControl)
        {
            _comboBoxPlugins = comboBoxPlugins;
            _pluginIndex = 1;
            _upperSize = 99999;
            _filterDate = DateTime.Now;
            _containingWord = "";
            _catalog = new AggregateCatalog();
            _pluginNames = new ObservableCollection<string>();

            _selectFolderObj = selectFolderObj;
            //Here we add all the parts found in all assemblies in directory of executing assembly directory
            //with file name matching Plugin*.dll
    

            RefreshPlugins();

            PluginView = _pluginControls[0];

            FileReader fr = new FileReader();
           _filesToSearching = new List<FileToSearch>();
            _filesToSearchingInCurrentDir=new List<FileToSearch>();

            //string path=sfd.SelectFolder();
            fr.GetFilesToShow(@"F:\music new", _filesToSearching);
            //fr.GetFilesToShow(path, lf);
            fr.GetFilesToShowInCurrentDir(@"F:\music new", _filesToSearchingInCurrentDir);
            _filesToShow = new ObservableCollection<FileToSearch>(_filesToSearching);

            _searchFilesCommand = new DelegateCommand(() =>
            {
                if (_filesToSearching != null)
                {
                    if (_isSubfolders)
                    {
                        _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                    }
                    else
                    {
                        _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearchingInCurrentDir, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, IsStopSearch);
                    }
                }
            });

            _changePluginCommand = new DelegateCommand(() =>
            {
                _pluginIndex = _comboBoxPlugins.SelectedIndex;
                PluginViewVar = _pluginControls[_pluginIndex];
                contentControl.Content=PluginView;
            });
            _refreshPluginCommand = new DelegateCommand(() =>
            {
                RefreshPlugins();
            });
            _stopSearchCommand = new DelegateCommand(() =>
            {
                _isStopSearch = true;
            });

            _changeFolderCommand = new DelegateCommand(() =>
            {
                _searchingPath=_selectFolderObj.SelectFolder();
            });
        }


        private void RefreshPlugins()
        {
            _catalog = new AggregateCatalog();
            string pluginsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            pluginsPath = Path.Combine(pluginsPath, "plugins");
            if (!Directory.Exists(pluginsPath))
                Directory.CreateDirectory(pluginsPath);
            _catalog.Catalogs.Add(new DirectoryCatalog(pluginsPath, "Plugin*.dll"));
            _container = new CompositionContainer(_catalog);
            _container.ComposeParts(this);

                PluginNames=new ObservableCollection<string>();
            _pluginControls = new List<PluginCommon.IView>();

            foreach (var pluginControl in Plugins)
            {
                _pluginControls.Add(pluginControl.Value);
                PluginNames.Add(pluginControl.Metadata.Name);
            }
            _comboBoxPlugins.Items.Refresh();

            foreach (var method in SearchMethod)
            {
                _methods.Add(method.Value);
            }
        }

        private SelectFolderDialog _selectFolderObj;
        private List<FileToSearch> _filesToSearching;
        private List<FileToSearch> _filesToSearchingInCurrentDir;
        private string _searchingPath="";

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
        public bool IsCurrentFolder
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
