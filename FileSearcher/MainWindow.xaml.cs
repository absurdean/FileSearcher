using System.Windows;
using FileSearcher.ViewModels;
using FileSearcher.Models;
using PluginCommon;

namespace FileSearcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PluginCommon.DataGridObject dataGrid = new PluginCommon.DataGridObject(dataGridFiles);
            SelectFolderDialog selectFolderObj = new SelectFolderDialog(this,dataGrid);
            PluginHost pluginHost = new PluginHost(contentControl, comboBoxPlugins);
            DataContext = new MainViewModel(this,selectFolderObj,pluginHost);
        }
    }
}
