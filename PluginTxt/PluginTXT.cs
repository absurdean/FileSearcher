using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PluginCommon;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows;
using System.Threading;
using System.Windows.Data;

namespace PluginTxt
{
    [Export(typeof(IPlugin))]
    public class PluginTXT : IPlugin
    {
        public void SearchFiles(ObservableCollection<FileToSearch> filteringFiles, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, bool stop)
        {
            stop = false;
            filteringFiles.Clear();
            foreach (var file in filesToSearch)
            {
                if (!stop)
                {
                    if ((file.Name.EndsWith(".txt")) && (file.Size >= lowerSize) && (file.Size <= upperSize) && (file.LastChangingDate <= filterDate)&&(SearchManager.ContainsWord(file.Path, specialAttribute)))
                    {
                        filteringFiles.Add(file);
                    }
                }
            }
        }
    }
}
