using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PluginCommon;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.IO;

namespace PluginMp3
{
    [Export(typeof(IPlugin))]
    public class PluginMP3 : IPlugin
    {
        public void SearchFiles(ObservableCollection<FileToSearch> filteringFiles, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, DataGridObject dataGridFiles)
        {
            foreach (var file in filesToSearch)
            {
                if (file.Name.EndsWith(".mp3"))
                {
                    if (File.Exists(file.Path))
                    {
                        ;//TagLib.File tagFile = TagLib.File.Create(file.Path);
                    }
                   
                }
            }
            MessageBox.Show("OK");
        }
    }
}
