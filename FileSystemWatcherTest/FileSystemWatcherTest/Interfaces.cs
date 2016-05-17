using System;
using System.Collections.Generic;

namespace FileSystemWatcherTest
{
    public interface IBaseManager<T> where T : class
    {
        void Save(T entity);
        void Remove(Guid id);
        List<T> List();
        T Get(Guid id);
    }

    //public interface IFileManager : IBaseManager<File>
    //{
    //    void UpdateFiles();
    //    void AddFiles()
    //}
}