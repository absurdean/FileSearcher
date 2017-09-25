using System;

namespace PluginCommon
{
    public class FileToSearch
    {
        public FileToSearch(string name, double size,DateTime date)
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
