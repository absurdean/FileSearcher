using System.Windows.Controls;

namespace FileSearcher.Models
{
    public class ContentControlObject
    {
        public ContentControl ContentControl { get; set; }
        public ContentControlObject(ContentControl contentControl)
        {
            ContentControl=contentControl;
        }
    }
}
