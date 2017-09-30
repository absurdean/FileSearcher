using System.Windows;
using System.Windows.Forms;
using PluginCommon;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileSearcher.Models
{
    public class SelectFolderDialog
    {
        private Window _owner;
        private PluginCommon.DataGridObject _dataGrid;
        private FileReader _fileReader;
        public SelectFolderDialog(Window owner, PluginCommon.DataGridObject dataGrid)
        {
            _owner = owner;
            _dataGrid = dataGrid;
            _fileReader = new FileReader();
        }
        public string SelectFolder()
        {

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = @"Select Directory";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                return folderBrowser.SelectedPath;
            }
            return null;
        }

        public void ChangeFolder(List<FileToSearch> filesToSearching, List<FileToSearch> filesToSearchingInCurrentDir, ObservableCollection<FileToSearch> filesToShow)
        {
            var selectedFolder = SelectFolder();
            if (selectedFolder != null)
            {
                filesToShow.Clear();
                filesToSearching.Clear();
                filesToSearchingInCurrentDir.Clear();
                _fileReader.GetFilesToShow(selectedFolder, filesToSearching);
                _fileReader.GetFilesToShowInCurrentDir(selectedFolder, filesToSearchingInCurrentDir);
                foreach (var file in filesToSearching)
                {
                    filesToShow.Add(file);
                }
            }
        }
    }
}
