using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PluginCommon
{
    public interface IPlugin
    {
        void SearchFiles(ObservableCollection<FileToSearch> filteringFiles, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, DataGridObject dataGridFiles);
    }
}
