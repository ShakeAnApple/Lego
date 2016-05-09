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
    public class LogFile
    {
        public string FullPath { get; set; }
        public List<LogLine> Content { get; set; }
    }

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

        public void RegisterWatcher(string path, bool includeSubdirs)
        {
            if (!includeSubdirs)
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
        private List<IFilesFilter> _filters; 

        private const string F_PATH = "path";
        private const string F_CONTENT = "content";

        // hardcooooooode
        private const string FILES_TO_INDEX_EXTENSION = "*.log";

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

        public FilesIndexer(string path, bool includeSubdirectories, List<IFilesFilter> filters)
            : this()
        {
            AddPath(path, includeSubdirectories);
            _filters = filters;
        }

        private void AddPath(string path, bool includeSubdirs)
        {
            base.RegisterWatcher(path, includeSubdirs);
            
            var filesPathsToIndex = DirectoryHelper.GetFilesByExtension(path, FILES_TO_INDEX_EXTENSION, includeSubdirs);
            var files = new List<LogFile>();
            foreach (var filePath in filesPathsToIndex)
            {
                files.Add(new LogFile
                {
                    FullPath = filePath.FullName,
                    Content = FileContentPreprocessor.GetLogLines(filePath.FullName)
                });
            }
            this.UpdateIndex(files);
        }

        public LuceneDirectory GetIndexDir()
        {
            return _indxDir;
        }

        public void AddPaths(string[] paths, bool includeSubdirs = false)
        {
            foreach (var path in paths)
            {
                AddPath(path, includeSubdirs);
            }

            _indxWriter.Commit();
        }

        private void UpdateIndex(List<LogFile> files)
        {
            foreach (var file in files)
            {
                CreateDoc(file).ForEach(_indxWriter.AddDocument);
            }
        }

        private List<Document> CreateDoc(LogFile file)
        {
            var docs = new List<Document>();
            foreach (var line in file.Content)
            {
                var doc = new Document();
                doc.Add(new Field(IndexFields.Path, file.FullPath, Field.Store.YES, Field.Index.NO));
                doc.Add(new Field(IndexFields.Date, line.Date, Field.Store.YES, Field.Index.NO));
                doc.Add(new Field(IndexFields.Status, line.Status.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field(IndexFields.Message, line.Message, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field(IndexFields.Thread, line.Thread.ToString(), Field.Store.YES, Field.Index.NO));
                docs.Add(doc);
            }
            return docs;
        }

        #region watcher impl
        // TODO handle subfolders everywhere (some setting?)
        
        protected override void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var paths = DirectoryHelper.GetFilesByExtension(e.FullPath, FILES_TO_INDEX_EXTENSION);
            //this.UpdateIndex(paths);
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