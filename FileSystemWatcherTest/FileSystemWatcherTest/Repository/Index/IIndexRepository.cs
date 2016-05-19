using System;

namespace FileSystemWatcherTest.Repository.Index
{
    public interface IIndexRepository
    {
        void AddFile(File file);
        void RemoveFile(string filePath);
        void Update(string filePath, string fieldName, string newValue);
    }

}