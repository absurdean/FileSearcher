using System.IO;
using System.Text;

namespace PluginTxt
{
     public static class SearchManager
    {
        public static bool ContainsWord(string file, string attribute)
        {
            if (File.Exists(file))
            {
                using (var reader = new StreamReader(file, Encoding.Default))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line.Contains(attribute))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;  
        }
    }
}
