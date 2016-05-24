    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.IO;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text;
    using FileSystemWatcherTest.Repository;
    using FileSystemWatcherTest.Repository.Database;
    using DB = FileSystemWatcherTest.Repository.Database;

    namespace FileSystemWatcherTest
    {
        public class FileProcessor : IEntityProcessor<File>
        {
            //TODO: get it from profile
            private Dictionary<string, long> _fileStreamPos;

            public FileProcessor()
            {
                _fileStreamPos = new Dictionary<string, long>();
            }

            public void Save(File file)
            {
                using (var uow = new UnitOfWork())
                {
                    var indexRepo = uow.GetIndexRepository();
                    var fileRepo = uow.GetRepository<DB.File>();
                    var cache = MemoryCache.Default;

                    long lastFilePos = 0;
                    if (!_fileStreamPos.TryGetValue(file.FullName, out lastFilePos))
                    {
                        _fileStreamPos.Add(file.FullName, 0);
                    }

                    string[] content;
                    
                    using (var fs = new FileStream(file.FullName))
                    {
                        if (lastFilePos == fs.Length)
                        {
                            return;
                        }

                        fs.Seek(lastFilePos, SeekOrigin.Begin);

                        var ch = fs.ReadByte();
                        while (ch != -1)
                        {
                            
                            do    
                        }
                        //var bytes = new byte[(int)(fs.Length - lastFilePos)];
                        //fs.Read(bytes, 0, (int)(fs.Length - lastFilePos));
                        ////TODO: UTF8??
                        //content = Encoding.UTF8.GetString(bytes).Split('\n');

                        _fileStreamPos[file.FullName] = fs.Length;
                    }
                    

                    //TODO: use name as id? 
                    var cacheKey = CacheHelper.GetCacheKey<DB.File>(file.FullName);
                    var dbFile = cache.Get(cacheKey) as DB.File;
                    if (dbFile == null)
                    {
                        dbFile = fileRepo.Get(file.Id);
                    }
                    if (dbFile == null)
                    {
                        dbFile = new DB.File
                        {
                            Id = Guid.NewGuid(),
                            FullName = file.FullName
                        };
                        fileRepo.Add(dbFile);
                        //cache.Add(cacheKey, dbFile, )
                        //    cache.CreateCacheEntryChangeMonitor()
                    }
                }
            }

            public void Remove(Guid id)
            {
                using (var uow = new UnitOfWork())
                {
                    var indexRepo = uow.GetIndexRepository();

                }
            }

            public List<File> List()
            {
                throw new NotImplementedException();
            }
        }
    }
}