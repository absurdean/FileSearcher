using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCommon;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PluginMp3;
using System.Linq;
using System.IO;
using System.Reflection;

namespace PluginMP3LibTesting
{
    [TestClass]
    public class PluginMP3TestCase
    {

        private List<FileToSearch> _filesToSearching= new List<FileToSearch>();
        public ObservableCollection<FileToSearch> FilesToShow= new ObservableCollection<FileToSearch>();
        private bool _isStopSearch = false;
        private string _pathForTesting = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "testfiles");

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
        public void MP3RightArtistNameTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting,_filesToSearching);
            var rightArtist = "AFI";
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightArtist, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[0];
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        public void MP3EmptyArtistNameTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            string artistName = "";
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, artistName, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow.Count();
            var excepted = _filesToSearching.Count();
            //should view all files, where is any artist name (not null)
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void MP3DifferentRegisterArtistNameTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightArtist = "tHe DoOrS";
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightArtist, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[3];
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void MP3ContainsStringArtistNameTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var rightArtist = "a";
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, rightArtist, 0, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow.Count();
            var excepted = 3;
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void MP3UpperSizeTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var lowestOne= _filesToSearching.Select((value, index) => new { Value = value.Size, Index = index })
                                            .Aggregate((a, b) => (a.Value < b.Value) ? a : b)
                                            .Index;
            var upperSize = _filesToSearching[lowestOne].Size;
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", 0, upperSize, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[lowestOne];
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        public void MP3LowerSizeTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var biggestOne=_filesToSearching.Select((value, index) => new { Value = value.Size, Index = index })
                                            .Aggregate((a, b) => (a.Value > b.Value) ? a : b)
                                            .Index;
            var lowerSize = _filesToSearching[biggestOne].Size;
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", lowerSize, 99999999, DateTime.Now, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[biggestOne];
            Assert.AreEqual(excepted, actual);
        }

        public void MP3OldDateTest()
        {
            (new FileSearcher.Models.FileReader()).GetFilesToShow(_pathForTesting, _filesToSearching);
            var oldestOne = _filesToSearching.Select((value, index) => new { Value = value.LastChangingDate, Index = index })
                                           .Aggregate((a, b) => (a.Value < b.Value) ? a : b)
                                           .Index;
            var date = _filesToSearching[oldestOne].LastChangingDate;
            (new PluginMP3()).SearchFiles(AddFileAndRefresh, _filesToSearching, "", 0, 99999999, date, _isStopSearch);
            var actual = FilesToShow[0];
            var excepted = _filesToSearching[oldestOne];
            Assert.AreEqual(excepted, actual);
        }
    }
}
