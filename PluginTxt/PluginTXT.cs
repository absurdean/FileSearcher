using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginCommon;
using System.ComponentModel.Composition;
using System.Windows;

namespace PluginTxt
{
    [Export(typeof(IPlugin))]
    public class PluginTXT : IPlugin
    {
        public void SearchFiles(ObservableCollection<FileToSearch> filteringFiles, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate)
        {
            MessageBox.Show("Wonderful");     
        }
    }
}
