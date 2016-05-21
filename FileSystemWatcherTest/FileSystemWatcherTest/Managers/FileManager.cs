using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using FileSystemWatcherTest.Repository;
using FileSystemWatcherTest.Repository.Database;
using DBFile = FileSystemWatcherTest.Repository.Database.File;
using DBMessage = FileSystemWatcherTest.Repository.Database.Message;

namespace FileSystemWatcherTest.Managers
{
    public class FileManager : IBaseManager<File>
    {
        private Dictionary<string, long> _fileStreamPos;

        public FileManager()
        {
            _fileStreamPos = new Dictionary<string, long>();
        } 
        
        public void Save(File file)
        {
            using (var uow = new UnitOfWork())
            {
                var indexRepo = uow.GetIndexRepository();
                var fileRepo = uow.GetRepository<DBFile>();
                var cache = MemoryCache.Default;

                long lastFilePos = 0;
                if (!_fileStreamPos.TryGetValue(file.FullName, out lastFilePos))
                {
                    _fileStreamPos.Add(file.FullName, 0);
                }

                string[] content;
                if (lastFilePos == 0)
                {
                    content = System.IO.File.ReadAllLines(file.FullName);
                }
                else
                {
                    using (var fs = new FileStream(file.FullName, FileMode.Open))
                    {
                        if (lastFilePos == fs.Length)
                        {
                            return;
                        }
                        
                        fs.Seek(lastFilePos, SeekOrigin.Begin);

                        var bytes = new byte[(int) (fs.Length - lastFilePos)];
                        fs.Read(bytes, 0, (int) (fs.Length - lastFilePos));
                        //TODO: UTF8??
                        content = Encoding.UTF8.GetString(bytes).Split('\n');

                        _fileStreamPos[file.FullName] = fs.Length;
                    }
                }

                //TODO: use name as id? 
                var dbFile = cache.Get(CacheHelper.GetCacheKey<DBFile>(file.FullName)) as DBFile;
                if (dbFile == null)
                {
                    dbFile = fileRepo.Get(file.Id);
                }
                if (dbFile == null)
                {
                    dbFile = new DBFile
                    {
                        Id = Guid.NewGuid(),
                        FullName = file.FullName
                    };
                    fileRepo.Add(dbFile);
                }
                if (dbFile.Messages == null)
                {
                    dbFile.Messages = new EntitySet<DBMessage>();
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