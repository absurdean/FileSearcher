using PluginCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace FileSearcher.Models
{
    public class PluginHost
    {
        [ImportMany]
        public IEnumerable<Action<ObservableCollection<FileToSearch> , List<FileToSearch>, string , double , double , DateTime >> SearchFiles { get; set; }
    }
}
