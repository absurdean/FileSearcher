using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using PluginCommon;
using System.Windows;

namespace FileSearcher.Models
{
    public class SearchManager
    {
        //private DataGridObject _dataGridFiles;

        //public SearchManager(IDataGridObject dataGridFiles)
        //{
        //    _dataGridFiles = dataGridFiles as DataGridObject;
        //}
        //public static bool SearchFiles(Func<FileToSearch,bool> funcy,List<FileToSearch> filesToSearch, double lowerSize, double upperSize, DateTime filterDate,bool stop)
        //{
        //    //filteringFiles.Clear();
        //    foreach (var file in filesToSearch)
        //    {
        //        if(stop)
        //        {
        //            break;
        //        }
        //            if ((file.Size >= lowerSize) && (file.Size <= upperSize))
        //            {
        //                if (file.LastChangingDate <= filterDate)
        //                {
        //                Thread.Sleep(1000);
        //                //owner.Dispatcher.Invoke(new Action(() => {
        //                //    filteringFiles.Add(file);
        //                //}));
        //                //owner.Dispatcher.Invoke(stop=funcy(file));
        //                stop = funcy.Invoke(file);
        //            }
        //        }
        //    }
        //    MessageBox.Show("Searching completed!");
        //    return true;
        //}
    }
}
