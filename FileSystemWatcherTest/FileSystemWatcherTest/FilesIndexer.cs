using System;
using System.Collections.Generic;
using System.IO;
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
        private Dictionary<string, FileSystemWatcher> _fsWatchers;

        protected MultipleDirectoriesWatcher()
        {
            _fsWatchers = new Dictionary<string, FileSystemWatcher>();
        }

        protected abstract void OnFileCreated(object sender, FileSystemEventArgs e);
        protected abstract void OnFileRenamed(object sender, RenamedEventArgs e);
        protected abstract void OnFileRemoved(object sender, FileSystemEventArgs e);

        private void RegisterSingleWatcher(string path)
        {
            if (_fsWatchers.ContainsKey(path))
            {
                return;
            }

            var watcher = new FileSystemWatcher(path);
            _fsWatchers.Add(path, watcher);

            watcher.Created += OnFileCreated;
            watcher.Renamed += OnFileRenamed;
            watcher.Deleted += OnFileRemoved;
            watcher.EnableRaisingEvents = true;
        }

        public void RegisterWatcher(string path, bool includeSubdirectories)
        {
            if (!includeSubdirectories)
            {
                RegisterSingleWatcher(path);
                return;
            }

            var paths = DirectoryHelper.ExpandAll(path);
            foreach (var p in paths)
            {
                RegisterSingleWatcher(p);
            }
        }

        public void Dispose()
        {
            foreach (var watcher in _fsWatchers)
            {
                watcher.Value.Dispose();
            }
        }
    }

    public class FilesIndexer : MultipleDirectoriesWatcher, IDisposable
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

        private void AddPath(string path, bool includeSubdirs)
        {
            base.RegisterWatcher(path, includeSubdirs);
            
            var filesToIndex = DirectoryHelper.GetFilesByExtension(path, FILES_TO_INDEX_EXTENSION, includeSubdirs);
            this.UpdateIndex(filesToIndex);
        }

        public void AddPaths(string[] paths, bool includeSubdirs = false)
        {
            foreach (var path in paths)
            {
                AddPath(path, includeSubdirs);
            }

            _indxWriter.Flush(true, false, true);
        }

        private void UpdateIndex(FileInfo[] files)
        {
            foreach (var file in files)
            {
                _indxWriter.AddDocument(CreateDoc(file));
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
            var paths = DirectoryHelper.GetFilesByExtension(e.FullPath, FILES_TO_INDEX_EXTENSION);
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

        void IDisposable.Dispose()
        {
            _indxWriter.Dispose();
            _indxDir.Dispose();
            
            base.Dispose();
        }
    }

}