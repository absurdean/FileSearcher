using System.Windows;
using System.Windows.Forms;

namespace FileSearcher.Models
{
    public class SelectFolderDialog
    {
        private Window _owner;
        public SelectFolderDialog(Window owner)
        {
            _owner = owner;
        }
        public string SelectFolder()
        {

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = @"Select Directory";
            if (folderBrowser.ShowDialog(_owner.GetIWin32Window()) == DialogResult.OK)
            {
                return folderBrowser.SelectedPath;
            }
            return null;
        }
    }
}
