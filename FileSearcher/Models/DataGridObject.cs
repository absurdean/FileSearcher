using System.Windows.Controls;

namespace FileSearcher.Models
{
    public class DataGridObject:IDataGridObject
    {
        public DataGrid DataGrid { get; set; }
        public DataGridObject(DataGrid dataGrid)
        {
            DataGrid = dataGrid;
        }
    }
}
