using System.Windows.Controls;

namespace FileSearcher.Models
{
    public class ComboBoxObject
    {
        public ComboBox ComboBox { get; set; }
        public ComboBoxObject(ComboBox comboBox)
        {
            ComboBox = comboBox ;
        }
    }
}
