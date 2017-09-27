using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginMp3;
using System.Collections.ObjectModel;
using PluginCommon;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginMp3.PluginMP3 pl3 = new PluginMP3();
            FileToSearch song = new FileToSearch("04 - Lithium.mp3", 0, DateTime.Now, @"F:\music new\Nirvana\2008 - Greatest Hits\CD104 - Lithium.mp3");
            List<FileToSearch> filesToSearch = new List<FileToSearch>();
            filesToSearch.Add(song);
            pl3.SearchFiles(null, filesToSearch, "Lithium", 0, 0, DateTime.Now, null);
        }
    }
}
