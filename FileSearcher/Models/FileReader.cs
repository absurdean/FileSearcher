using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FileSearcher.Models
{
    public class FileReader
    {
        public void GetFilesToShow(string path, List<File> filesToShow)
        {
            //List<File> filesToShow = new List<File>();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    filesToShow.Add(new File(f.Name,f.Length,f.LastWriteTime));
                    //text.WriteLine(f.Name);
                    //Console.WriteLine(f.Name);
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
           // return filesToShow;
        }

        public void GetFilesToShowInCurrentDir(string path, List<File> filesToShowInCurrentDir)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    filesToShowInCurrentDir.Add(new File(f.Name,f.Length,f.LastWriteTime));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


    }
}
