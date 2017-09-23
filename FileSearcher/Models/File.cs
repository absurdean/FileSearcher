using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher.Models
{
    public class File
    {
        public File(string name, double size,DateTime date)
        {
            Name = name;
            Size = size;
            LastChangingDate = date;
        }
        public string Name { get; set; }
        public double Size { get; set; }
        public DateTime LastChangingDate { get; set; }
    }
}
