using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PluginCommon
{
    /// <summary>
    /// <param name="filteringFiles">files to show in datagrid, clear before add objects</param>
    /// 
    /// </summary>
    public interface IPlugin
    {
        bool SearchFiles(Func<FileToSearch, bool> addFilesAndRefresh, List<FileToSearch> filesToSearch,string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, bool stop);
    }
}
