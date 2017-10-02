using System;
using System.Collections.Generic;
using PluginCommon;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Threading;

namespace PluginMp3
{
    [Export(typeof(IPlugin))]
    public class PluginMP3 : IPlugin
    {
        public bool SearchFiles(Func<FileToSearch, bool> addFilesAndRefresh, List<FileToSearch> filesToSearch,string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, bool stop)
        {
            try
            {
                foreach (var file in filesToSearch)
                {
                    if(stop)
                    {
                        break;
                    }
                    if (file.Name.ToLower().EndsWith(".mp3") && (file.Size >= lowerSize) && (file.Size <= upperSize) && (file.LastChangingDate <= filterDate))
                    {
                        if (File.Exists(file.Path))
                        {
                            var tagFile = TagLib.File.Create(file.Path);
                            if ((tagFile.Tag.FirstAlbumArtist != null) && (tagFile.Tag.FirstAlbumArtist.ToLower().Trim().Contains(specialAttribute.Trim().ToLower().Trim())))
                            {
                                Thread.Sleep(100);
                                stop=addFilesAndRefresh.Invoke(file);
                            }
                        }
                    }
                }
                if (!stop)
                {
                    MessageBox.Show("Searching completed!", " ", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                return true;
            }
            catch (FileNotFoundException ex1)
            {
                MessageBox.Show("FileNotFoundException:" + ex1.Message);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with searching .mp3 file:" + ex.Message);
                return true;
            }
        }
    }
}
