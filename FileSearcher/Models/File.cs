using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher.Models
{
    public class File
    {
        public File(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
