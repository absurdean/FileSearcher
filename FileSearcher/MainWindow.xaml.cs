using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            PluginCommon.DataGridObject dataGridObject = new PluginCommon.DataGridObject(dataGridFiles);
            //SearchManager searchManager = new SearchManager(dataGridObject);
            DataContext = new MainViewModel(null,comboBoxPlugins, contentControl,dataGridObject);
        }
    }
}
