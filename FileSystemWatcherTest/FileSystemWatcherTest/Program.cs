using System;
using System.Data.Linq;
using FileSystemWatcherTest.Repository;

namespace FileSystemWatcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //  var files = new[] {"paths to log files"};
            //var logs = new Parser

            //GraphDataContext.Test();

            var fId = Guid.NewGuid();
            var fie = new Repository.Database.File
            {
                Id = fId,
                FullName = "ololo",
                Messages = new EntitySet<Repository.Database.Message>()
            };
            var lol = new LolRepo();
            lol.Save(fie);
          //  var dbFile = lol.Get(fId);
           // Console.WriteLine(dbFile.FullName);
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
