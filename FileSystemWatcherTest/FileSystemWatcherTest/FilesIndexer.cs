using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemWatcherTest
{
    public abstract class MultipleDirectoriesWatcher : IDisposable
    {
        private List<FileSystemWatcher> _fsWatchers;
        private HashSet<string> _pathsToWatch;

        protected MultipleDirectoriesWatcher()
        {
            _fsWatchers = new List<FileSystemWatcher>();
            _pathsToWatch = new HashSet<string>();       
        }

        protected abstract void OnFileCreated(object sender, FileSystemEventArgs e);
        protected abstract void OnFileRenamed(object sender, RenamedEventArgs e);
        protected abstract void OnFileRemoved(object sender, FileSystemEventArgs e);

        protected bool RegisterWatcher(string path, bool includeSubdirectories)
        {
            // TODO deep subWatch?
            // TODO expand subfolders
            if (!_pathsToWatch.Add(path))
            {
                return false;
            }

            var watcher = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = includeSubdirectories
            };
            watcher.Created += OnFileCreated;
            watcher.Renamed += OnFileRenamed;
            watcher.Deleted += OnFileRemoved;

            _fsWatchers.Add(watcher);
            return true;
        }

        public void Dispose()
        {
            foreach (var watcher in _fsWatchers)
            {
                watcher.Dispose();
            }
        }
    }

    public class FilesIndexer : MultipleDirectoriesWatcher
    {
        
        public FilesIndexer()
        {
        }

        public FilesIndexer(string path, bool includeSubdirectories) 
            : this()
        {
            AddPath(path, includeSubdirectories);
        }

        public bool AddPath(string path, bool includeSubdirectories = false)
        {
            if (!base.RegisterWatcher(path, includeSubdirectories))
            {
                return false;
            }

            this.UpdateIndex();
            return true;
        }

        public bool AddPaths(string[] paths, bool includeSubdirectories = false)
        {
            foreach (var path in paths)
            {
                if (!AddPath(path, includeSubdirectories))
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateIndex()
        {
        }

        #region watcher impl
        protected override void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            this.UpdateIndex();
        }

        protected override void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            this.UpdateIndex();
        }

        protected override void OnFileRemoved(object sender, FileSystemEventArgs e)
        {
            this.UpdateIndex();
        }
        #endregion
    }

}