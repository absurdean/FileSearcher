﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PluginCommon;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PluginMp3
{
    [Export(typeof(IPlugin))]
    public class PluginMP3 : IPlugin
    {
        public async void SearchFiles(ObservableCollection<FileToSearch> filteringFiles, List<FileToSearch> filesToSearch, string specialAttribute, double lowerSize, double upperSize, DateTime filterDate,bool stop)
        {
                try
                {
                    filteringFiles.Clear();
                    foreach (var file in filesToSearch)
                    {
                        if(stop)
                        {
                            break;
                        }
                        if (file.Name.EndsWith(".mp3") && (file.Size >= lowerSize) && (file.Size <= upperSize) && (file.LastChangingDate <= filterDate))
                        {
                            if (File.Exists(file.Path))
                            {
                                var tagFile = TagLib.File.Create(file.Path);
                                if ((tagFile.Tag.FirstAlbumArtist != null) && (tagFile.Tag.FirstAlbumArtist.ToLower().Contains(specialAttribute.Trim().ToLower())))
                                {
                                    filteringFiles.Add(file);
                                    await Task.Delay(10);
                                }
                            }
                        }
                    }
                MessageBox.Show("Searching completed");
                }
                catch (FileNotFoundException ex1)
                {
                    MessageBox.Show("FileNotFoundException:" + ex1.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error with searching .mp3 file:" + ex.Message);
                }
        }
    }
}
