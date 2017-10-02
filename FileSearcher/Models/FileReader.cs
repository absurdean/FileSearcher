using System;
using System.Collections.Generic;
using System.Windows;
using PluginCommon;
using System.IO;

namespace FileSearcher.Models
{
    public class FileReader
    {
        public void GetFilesToShow(string path, List<FileToSearch> filesToShow)
        {
            try
            {               
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    filesToShow.Add(new FileToSearch(f.Name,f.Length,f.LastWriteTime,f.FullName));
                }

                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    GetFilesToShow(path + "\\" + d.Name + "\\",filesToShow);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void GetFilesToShowInCurrentDir(string path, List<FileToSearch> filesToShowInCurrentDir)
        {
            try
            {
                if (filesToShowInCurrentDir != null)
                {
                    filesToShowInCurrentDir=new List<FileToSearch>();
                }
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    filesToShowInCurrentDir.Add(new FileToSearch(f.Name,f.Length,f.LastWriteTime,f.FullName));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
