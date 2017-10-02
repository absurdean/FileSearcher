using System.Windows;
using FileSearcher.ViewModels;
using FileSearcher.Models;
using PluginCommon;
using System.Collections.ObjectModel;

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
            ComboBoxObject comboBox = new ComboBoxObject(comboBoxPlugins);
            ContentControlObject contentControls = new ContentControlObject(contentControl);
            SelectFolderDialog selectFolderObj = new SelectFolderDialog(this);
            PluginHost pluginHost = new PluginHost(contentControls, comboBox);
            DataContext = new MainViewModel(this,selectFolderObj,pluginHost);
        }
    }
}
