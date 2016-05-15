using System;
using FileSystemWatcherTest.Repository;

namespace FileSystemWatcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //  var files = new[] {"paths to log files"};
            //var logs = new Parser

            GraphDataContext.Test();

            //// turn into messages
            //using (var indexer = new FilesIndexer())
            //{
            //    indexer.AddPaths(new [] {@"C:\Users\1\Desktop\test"});
            //    var searcher = new FilesSearcher(indexer.GetIndexDir());
            //    var found = searcher.Search("uid");
            //}
        }
    }

}
