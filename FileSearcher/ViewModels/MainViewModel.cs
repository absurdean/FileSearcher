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

namespace FileSearcher.ViewModels
{
    public class MainViewModel:BindableBase
    {

        private PluginCommon.IView PluginViewVar;
        // [Import(typeof(IView), AllowRecomposition = true, AllowDefault = true)]
        public PluginCommon.IView PluginView
        {
            get { return PluginViewVar; }
            set
            {
                PluginViewVar = value;
                OnPropertyChanged(nameof(PluginViewVar));
            }
        }

        [ImportMany(typeof(PluginCommon.IView), AllowRecomposition = true)]
        public IEnumerable<Lazy<PluginCommon.IView>> Plugins;

        [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
        public IEnumerable<Lazy<IPlugin>> SearchMethod;

        private AggregateCatalog catalog;
        private CompositionContainer container;


        public MainViewModel(SearchManager searchManager)
        {
            catalog = new AggregateCatalog();
           
            //Here we add all the parts found in all assemblies in directory of executing assembly directory
            //with file name matching Plugin*.dll
            string pluginsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            pluginsPath = Path.Combine(pluginsPath, "plugins");
            if (!Directory.Exists(pluginsPath))
                Directory.CreateDirectory(pluginsPath);
            catalog.Catalogs.Add(new DirectoryCatalog(pluginsPath, "Plugin*.dll"));

            container = new CompositionContainer(catalog);
            container.ComposeParts(this);



            var pluginContainer = Plugins.FirstOrDefault();
            if (pluginContainer != null)
            {
                PluginView = pluginContainer.Value;
            }



        _searchManager = searchManager;
            _upperSize = 20;
            _filterDate = DateTime.Now;
            _containingWord = "";
            FileReader fr = new FileReader();
           _filesToSearching = new List<FileToSearch>();
            _filesToSearchingInCurrentDir=new List<FileToSearch>();
            List<FileToSearch> cd = new List<FileToSearch>();
            SelectFolderDialog sfd = new SelectFolderDialog();
            //string path=sfd.SelectFolder();
            fr.GetFilesToShow(@"F:\bookfilter",_filesToSearching);
            //fr.GetFilesToShow(path, lf);
            fr.GetFilesToShowInCurrentDir(@"F:\bookfilter\www", _filesToSearchingInCurrentDir);
            _filesToShow = new ObservableCollection<FileToSearch>(_filesToSearching);
            _filesToShowInCurrentDir = new ObservableCollection<FileToSearch>(cd);

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
    }
}
