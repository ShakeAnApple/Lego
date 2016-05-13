using System.Collections.Generic;
using FileSystemWatcherTest.Models;

namespace FileSystemWatcherTest
{
    // watch file changes? not here?
    // watch the difference? (on refresh)
    public interface IParser
    {
        List<LogFile> GetLogs(IEnumerable<string> paths); 
    }

    public interface IFileIndexer
    {
        void AddToIndex(List<LogFile> files);
    }

    public interface ITagger
    {
        void GetTaggedMessages(List<LogFile> files);
    }
}