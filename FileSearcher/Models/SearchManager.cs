using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using PluginCommon;

namespace FileSearcher.Models
{
    public class SearchManager
    {
        private DataGridObject _dataGridFiles;

        public SearchManager(IDataGridObject dataGridFiles)
        {
            _dataGridFiles = dataGridFiles as DataGridObject;
        }
        public static void SearchFiles(ObservableCollection<FileToSearch> filteringFiles,List<FileToSearch> filesToSearch, double lowerSize, double upperSize, DateTime filterDate)
        {
            filteringFiles.Clear();
            foreach (var file in filesToSearch)
            {
                    if ((file.Size >= lowerSize) && (file.Size <= upperSize))
                    {
                        if (file.LastChangingDate <= filterDate)
                        {
                            filteringFiles.Add(file);
                    }
                }
            }
        }
    }
}
