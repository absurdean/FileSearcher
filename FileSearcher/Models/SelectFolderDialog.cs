using System.Windows.Forms;

namespace FileSearcher.Models
{
    public class SelectFolderDialog
    {
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
    }
}
