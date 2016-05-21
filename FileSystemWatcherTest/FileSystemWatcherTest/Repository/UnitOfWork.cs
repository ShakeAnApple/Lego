using System;
using FileSystemWatcherTest.Repository.Database;
using FileSystemWatcherTest.Repository.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Version = Lucene.Net.Util.Version;

namespace FileSystemWatcherTest.Repository
{
    public class UnitOfWork : IDisposable
    {
        private GraphDataContext _context;
        private IndexWriter _indexWriter;

        public UnitOfWork()
        {
            this.CreateContext();
        }

        private void CreateContext()
        {
            if (_context == null)
            {
                _context = new GraphDataContext(ConfigRepository.ConnectionString());
            }
        }

        public IRepository<T> GetRepository<T>() where T : DBEntity
        {
            return new Repository<T>
            {
                Context = _context
            };
        }

        public IIndexRepository GetIndexRepository()
        {
            if (_indexWriter == null)
            {
                var indxDir = ConfigRepository.IndexDirectory();
                _indexWriter = new IndexWriter(indxDir, new StandardAnalyzer(Version.LUCENE_30), 
                    !IndexReader.IndexExists(indxDir), IndexWriter.MaxFieldLength.LIMITED);
            }
            return new IndexRepository(_indexWriter);
        }

        public void Submit()
        {
            if (_context != null)
            {
                _context.SubmitChanges();
            }
            if (_indexWriter != null)
            {
                _indexWriter.Commit();
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

            //TODO think about dynamic case (dispose is greedy for time)
            if (_indexWriter != null)
            {
                _indexWriter.Dispose();
                _context = null;
            }
        }
    }
}