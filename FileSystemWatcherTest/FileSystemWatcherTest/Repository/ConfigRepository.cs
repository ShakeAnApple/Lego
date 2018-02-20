using System.Configuration;
using System.IO;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;

namespace FileSystemWatcherTest.Repository
{
    public class ConfigRepository
    {
        public static Directory IndexDirectory()
        {
            var dirName = ConfigurationManager.AppSettings["IndexDirName"];
            var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), dirName);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return new SimpleFSDirectory(new DirectoryInfo(path));
        }

        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}