using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PluginCommon;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows;

namespace PluginTxt
{
    [Export(typeof(IPlugin))]
    public class PluginTXT : IPlugin
    {
        public void SearchFiles(ObservableCollection<FileToSearch> filteringFiles, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, DataGridObject dataGridFiles)
        {
            filteringFiles.Clear();
            foreach (var file in filesToSearch)
            {
                if (file.Name.EndsWith(".txt"))
                {
                    if ((file.Size >= lowerSize) && (file.Size <= upperSize))
                    {
                        if (file.LastChangingDate <= filterDate)
                        {
                            if (SearchManager.ContainsWord(file.Path, specialAttribute))
                            {
                                filteringFiles.Add(file);
                                dataGridFiles.DataGrid.UpdateLayout();
                            }
                        }
                    }
                }
            }
        }
    }
}
