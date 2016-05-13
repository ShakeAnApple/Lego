using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemWatcherTest.Old
{
    // wtf -_-
    internal static class DirectoryHelper
    {
        public static string[] ExpandAll(string path)
        {
            throw new NotImplementedException();
        }

        public static List<FileInfo> GetFilesByExtension(string path, string extension, bool includeSubdirs = false)
        {
            var result = new List<FileInfo>();
            var filePathes = Directory.GetFiles(path, extension);
            foreach (var fpath in filePathes)
            {
                result.Add(new FileInfo(fpath));
            }
            if (includeSubdirs)
            {
                
            }
            return result;
        }
    }
}