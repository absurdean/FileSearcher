using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PluginCommon;
using System.Collections.ObjectModel;
using System.Reflection;
using PluginTxt;
using System.IO;
using System.Linq;

namespace PluginTXTLibTesting
{
    [TestClass]
    public class PluginTXTTestCase
    {
        private List<FileToSearch> _filesToSearching = new List<FileToSearch>();
        public ObservableCollection<FileToSearch> FilesToShow = new ObservableCollection<FileToSearch>();
        private bool _isStopSearch = false;
        private string _pathForTesting = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"testfiles");

        public bool AddFileAndRefresh(FileToSearch file)
        {
            FilesToShow.Add(file);
            if (_isStopSearch)
            {
                return true;
            }
            else { return false; }
        }

        [TestMethod]
        public void TXTRightWordAndTrimTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightWord = "search for TXTPlugin ";
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightWord, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[3];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTWrongWordTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightWord = "search for TXTPlaugin ";
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightWord, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow.Count;
            var excepted = 0;
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTRussianWordTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightWord = "Маяковский";
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightWord, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[1];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTDifferentRegisterWordTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightWord = "МаЯКоВсКиЙ";
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightWord, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[1];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTMultipleContainsWordTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightWord = "user";
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightWord, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow.Count;
            var excepted = 3;
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTUpperSizeTesting()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var lowestOne = _filesToSearching.Select((value, index) => new { Value = value.Size, Index = index })
                                           .Aggregate((a, b) => (a.Value < b.Value) ? a : b)
                                           .Index;
            var upperSize = _filesToSearching[lowestOne].Size;
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", 0, upperSize, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[lowestOne];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTLowerSizeTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var biggestOne = _filesToSearching.Select((value, index) => new { Value = value.Size, Index = index })
                                           .Aggregate((a, b) => (a.Value > b.Value) ? a : b)
                                           .Index;
            var upperSize = _filesToSearching[biggestOne].Size;
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", 0, upperSize, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[biggestOne];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTOldDateTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var oldestOne = _filesToSearching.Select((value, index) => new { Value = value.LastChangingDate, Index = index })
                                           .Aggregate((a, b) => (a.Value < b.Value) ? a : b)
                                           .Index;
            var date = _filesToSearching[oldestOne].LastChangingDate;
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", 0, 99999999, date, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[oldestOne];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void TXTFreshDateTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            (new PluginTXT()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow.Count;
            var excepted = _filesToSearching.Count;
            Assert.AreEqual(excepted, actual);
        }        
    }
}
