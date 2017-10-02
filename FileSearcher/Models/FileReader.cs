using System;
using System.Collections.Generic;
using System.Windows;
using PluginCommon;
using System.IO;

namespace FileSearcher.Models
{
    public class FileReader
    {
        public void GetFilesToShow(string path, List<FileToSearch> filesToSearching)
        {
            try
            {               
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    filesToSearching.Add(new FileToSearch(f.Name,f.Length,f.LastWriteTime,f.FullName));
                }

                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    GetFilesToShow(path + "\\" + d.Name + "\\", filesToSearching);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void GetFilesToShowInCurrentDir(string path, List<FileToSearch> filesToSearchingInCurrentDir)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    filesToSearchingInCurrentDir.Add(new FileToSearch(f.Name,f.Length,f.LastWriteTime,f.FullName));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
