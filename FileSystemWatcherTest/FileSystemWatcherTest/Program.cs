using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWatcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var indexer = new FilesIndexer())
            {
                indexer.AddPaths(new [] {@"C:\Users\1\Desktop\test"});
                var searcher = new FilesSearcher(indexer.GetIndexDir());
                var found = searcher.Search("uid");
                foreach (var match in found)
                {
                    Console.WriteLine(match.String);
                    Console.WriteLine();
                }
            }
        }
    }
}
