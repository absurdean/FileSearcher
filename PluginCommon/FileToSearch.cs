using System;

namespace PluginCommon
{
    public class FileToSearch
    {
        public FileToSearch(string name, double size,DateTime date, string path)
        {
            Name = name;
            Size = size;
            LastChangingDate = date;
            Path = path;
        }
        public string Name { get; set; }
        public double Size { get; set; }
        public DateTime LastChangingDate { get; set; }
        public string Path { get; set; }
    }
}
