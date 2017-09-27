using System.Windows.Controls;

namespace PluginCommon
{
    public class DataGridObject
    {
        public DataGrid DataGrid { get; set; }
        public DataGridObject(DataGrid dataGrid)
        {
            DataGrid = dataGrid;
        }
    }
}
