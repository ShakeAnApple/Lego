using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Directory = System.IO.Directory;
using LuceneDirectory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace FileSystemWatcherTest
{
    public abstract class MultipleDirectoriesWatcher : IDisposable
    {
        private List<FileSystemWatcher> _fsWatchers;
        // maybe tree?(usefull in case of directory rename) the whole tree for just a check? 
        private readonly HashSet<string> _pathsToWatch;

        protected MultipleDirectoriesWatcher()
        {
            _fsWatchers = new List<FileSystemWatcher>();
            _pathsToWatch = new HashSet<string>();       
        }

        protected abstract void OnFileCreated(object sender, FileSystemEventArgs e);
        protected abstract void OnFileRenamed(object sender, RenamedEventArgs e);
        protected abstract void OnFileRemoved(object sender, FileSystemEventArgs e);

        public bool RegisterWatcher(string path, bool includeSubdirectories)
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            foreach (var watcher in _fsWatchers)
            {
                watcher.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

    public class FilesIndexer : MultipleDirectoriesWatcher
    {
        private readonly IndexWriter _indxWriter;
        private readonly LuceneDirectory _indxDir;

        private const string F_PATH = "path";
        private const string F_CONTENT = "content";

        // hardcooooooode
        private const string FILES_TO_INDEX_EXTENSION = "*.txt";

        public FilesIndexer()
        {
            _indxDir = FSDirectory.Open(Directory.GetCurrentDirectory());
            _indxWriter = new IndexWriter(_indxDir, new StandardAnalyzer(Version.LUCENE_30), true, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public FilesIndexer(string path, bool includeSubdirectories) 
            : this()
        {
            AddPath(path, includeSubdirectories);
        }

        private bool AddPath(string path, bool includeSubdirectories)
        {
            if (!base.RegisterWatcher(path, includeSubdirectories))
            {
                return false;
            }

            var paths = DirsHelper.GetFilesByExtension(path, FILES_TO_INDEX_EXTENSION, includeSubdirectories);
            
            this.UpdateIndex(paths);
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
            _indxWriter.Commit();
            return true;
        }

        private void UpdateIndex(FileInfo[] files)
        {
            foreach (var file in files)
            {
                var doc = CreateDoc(file);
                _indxWriter.AddDocument(doc);
            }
        }

        private Document CreateDoc(FileInfo file)
        {
            var doc = new Document();
            doc.Add(new Field(F_PATH, file.DirectoryName, Field.Store.YES, Field.Index.NO));
            doc.Add(new Field(F_CONTENT, new StreamReader(file.OpenRead())));
            return doc;
        }

        #region watcher impl
        // TODO handle subfolders everywhere (some setting?)
        protected override void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var paths = DirsHelper.GetFilesByExtension(e.FullPath, FILES_TO_INDEX_EXTENSION);
            this.UpdateIndex(paths);
        }

        protected override void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            //todo update exact doc
            //todo update directories
            //fuck
            //this.UpdateIndex();
        }

        protected override void OnFileRemoved(object sender, FileSystemEventArgs e)
        {
            //todo delete doc from index
            // todo delete watcher?
            //this.UpdateIndex();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _indxWriter.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}