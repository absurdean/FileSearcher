using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Collections.ObjectModel;
using PluginCommon;
using FileSearcher;
using System.Collections.Generic;

namespace MP3SearchPluginTest
{
    [TestClass]
    public class UnitTest1
    {
        public ObservableCollection<FileToSearch> FilesToShow = new ObservableCollection<FileToSearch>();
        //private List<FileToSearch> _filesToSearching = new List<FileToSearch>()
        //{
        //    new FileToSearch("05-Heart-Shaped Box.mp3",),
        //    new FileToSearch("06  Light My Fire.mp3"),
        //    new FileToSearch("13 AFI - This Time Imperfect"),
        //    //new FileToSearch("")
        //};
        private List<FileToSearch> _filesToSearching; 
        
        private bool _isStopSearch=false;
        PluginMp3.PluginMP3 pluginMP3 = new PluginMp3.PluginMP3();
        public UnitTest1()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow("F:\\music new\\dif", _filesToSearching);
        }

        public bool AddFileAndRefresh(FileToSearch file)
        {
                FilesToShow.Add(file);
            if (_isStopSearch)
            { return true; }
            else { return false; }
        }


        [TestMethod]
        public void IsRightArtistTest()
        {
            string rightArtist = "AFI";
            pluginMP3.SearchFiles(AddFileAndRefresh, _filesToSearching, rightArtist, 0, 99999999, DateTime.Now,ref _isStopSearch);

            ObservableCollection<FileToSearch> expected = new ObservableCollection<FileToSearch>() { _filesToSearching[2] };
            ObservableCollection<FileToSearch> actual = FilesToShow;
            Assert.AreEqual(expected, actual);
        }
    }
}
