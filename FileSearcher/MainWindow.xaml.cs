using System.Windows;
using FileSearcher.ViewModels;
using FileSearcher.Models;

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
            SelectFolderDialog selectFolderObj = new SelectFolderDialog(this);
            PluginHost pluginHost = new PluginHost(contentControl, comboBoxPlugins);
            DataContext = new MainViewModel(this,selectFolderObj,pluginHost);
        }
    }
}
