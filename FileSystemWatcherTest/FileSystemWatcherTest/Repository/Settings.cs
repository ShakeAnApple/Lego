using System.IO;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;

namespace FileSystemWatcherTest.Repository
{
    public class Settings
    {
        public static Directory IndexDirectory()
        {
            var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Index");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return new SimpleFSDirectory(new DirectoryInfo(path));
        }
    }
}