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
        public List<PluginCommon.IView> PluginControls
        {
            get { return _pluginControls; }
            set
            {
                _pluginControls = value;
                OnPropertyChanged(nameof(_pluginControls));
            }
        }

        private List<string> _pluginNames;
        public List<string> PluginNames
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
        //убрать при рефакторе
        private ComboBox _comboBoxPlugins;

        public MainViewModel(SearchManager searchManager, ComboBox comboBoxPlugins, ContentControl contentControl, PluginCommon.DataGridObject dataGridFiles)
        {
            _comboBoxPlugins = comboBoxPlugins;
            _pluginIndex = 1;
            _upperSize = 99999;
            _filterDate = DateTime.Now;
            _containingWord = "";
            _catalog = new AggregateCatalog();
           
            
            //Here we add all the parts found in all assemblies in directory of executing assembly directory
            //with file name matching Plugin*.dll
            string pluginsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            pluginsPath = Path.Combine(pluginsPath, "plugins");
            if (!Directory.Exists(pluginsPath))
                Directory.CreateDirectory(pluginsPath);
            _catalog.Catalogs.Add(new DirectoryCatalog(pluginsPath, "Plugin*.dll"));

            RefreshPlugins();

            PluginView = PluginControls[0];
           
            _searchManager = searchManager;

            FileReader fr = new FileReader();
           _filesToSearching = new List<FileToSearch>();
            _filesToSearchingInCurrentDir=new List<FileToSearch>();
            List<FileToSearch> cd = new List<FileToSearch>();
            SelectFolderDialog sfd = new SelectFolderDialog();
            //string path=sfd.SelectFolder();
            fr.GetFilesToShow(@"F:\bookfilter\www",_filesToSearching);
            //fr.GetFilesToShow(path, lf);
            fr.GetFilesToShowInCurrentDir(@"F:\bookfilter\www", _filesToSearchingInCurrentDir);
            _filesToShow = new ObservableCollection<FileToSearch>(_filesToSearching);
            _filesToShowInCurrentDir = new ObservableCollection<FileToSearch>(cd);

            _searchFilesCommand = new DelegateCommand(() =>
            {
                if (_isSubfolders)
                {
                    _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearching, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate, dataGridFiles);//method.SearchFiles(FilesToShow, _filesToSearchingInCurrentDir, _containingWord, LowerSize, UpperSize, FilterDate);
                }
                else
                {
                    _methods[_pluginIndex].SearchFiles(FilesToShow, _filesToSearchingInCurrentDir, PluginView.SpecialAttribute, LowerSize, UpperSize, FilterDate,dataGridFiles);
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
        }


        private void RefreshPlugins()
        {
            _container = new CompositionContainer(_catalog);
            _container.ComposeParts(this);
            PluginNames = new List<string>();
            PluginControls = new List<PluginCommon.IView>();

            foreach (var pluginControl in Plugins)
            {
                PluginControls.Add(pluginControl.Value);
                PluginNames.Add(pluginControl.Metadata.Name);
            }

            foreach (var method in SearchMethod)
            {
                _methods.Add(method.Value);
            }
        }



        private SearchManager _searchManager;
        private List<FileToSearch> _filesToSearching;
        private List<FileToSearch> _filesToSearchingInCurrentDir;
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

        private ObservableCollection<FileToSearch> _filesToShowInCurrentDir;
        public ObservableCollection<FileToSearch> FilesToShowInCurrentDir
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
    }
}
