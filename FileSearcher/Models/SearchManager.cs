using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FileSearcher.Models
{
    public class SearchManager
    {
        private DataGridObject _dataGridFiles;

        public SearchManager(IDataGridObject dataGridFiles)
        {
            _dataGridFiles = dataGridFiles as DataGridObject;
        }
        public void SearchFiles(ObservableCollection<File> filteringFiles,List<File> filesToSearch,string containingWord, double lowerSize, double upperSize, DateTime filterDate)
        {
            filteringFiles.Clear();
            foreach (var file in filesToSearch)
            {
                if (file.Name.Contains(containingWord))
                {
                    if ((file.Size >= lowerSize) && (file.Size <= upperSize))
                    {
                        if (file.LastChangingDate <= filterDate)
                        {
                            filteringFiles.Add(file);
                            _dataGridFiles.DataGrid.UpdateLayout();
                            Task.Delay(3000);
                        }
                    }
                }
            }
        }
    }
}
