using System;
using System.Collections.Generic;
using PluginCommon;
using System.ComponentModel.Composition;
using System.Windows;
using System.Threading;

namespace PluginTxt
{
    [Export(typeof(IPlugin))]
    public class PluginTXT : IPlugin
    {
        public bool SearchFiles(Func<FileToSearch, bool> addFilesAndRefresh, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate,bool stop)
        {
            try
            {
                if (upperSize == 99999999)
                {
                    upperSize = 99999999999;
                }
                foreach (var file in filesToSearch)
                {
                    if (stop)
                    {
                        break;
                    }
                    if ((file.Name.ToLower().EndsWith(".txt")) && (file.Size >= lowerSize) && (file.Size <= upperSize) && (file.LastChangingDate <= filterDate) && (SearchManager.ContainsWord(file.Path, specialAttribute)))
                    {
                        Thread.Sleep(100);
                        stop = addFilesAndRefresh.Invoke(file);
                    }
                }
                if (!stop)
                {
                    MessageBox.Show("Searching completed!", " ", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error with searching .mp3 file:" + ex.Message);
                return true;
            }
        }
    }
}
