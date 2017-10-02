using System;
using System.Collections.Generic;

namespace PluginCommon
{
    public interface IPlugin
    {
        /// <summary>
        ///main function to search for plugins
        ///to make your plugin available, put it on the ../bin/Debug(Release)/plugins folder
        /// </summary>
        /// <param name="filesToSearch">list of all files to search</param>
        /// <param name="addFilesAndRefresh">func that make addition files in collection to
        ///                                  show in datagrid, returns bool, to check stop flag</param>
        /// <param name="filterDate">search before that date</param>
        /// <param name="upperSize">upper size of file to search(in bytes), if value==99999999 that's maximim,
        ///                         may search greater than that(that's for user-fiendly scale in size control)</param>
        /// <param name="lowerSize">lower size of file to search(in bytes)</param>
        /// <param name="specialAttribute">string, that user writes in plugin control</param>
        /// <param name="stop">flag to stop searching from ui, check it at the begining
        ///                     of searching cycle to quit and initialize it by
        ///                     addFilesAndRefresh func as return value</param>

        bool SearchFiles(Func<FileToSearch, bool> addFilesAndRefresh, List<FileToSearch> filesToSearch,string specialAttribute, double lowerSize, double upperSize, DateTime filterDate, bool stop);
    }
}
