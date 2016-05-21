using System;
using System.Linq;

namespace FileSystemWatcherTest.Repository.Database
{
    public interface IRepository<T> where T : DBEntity
    {
        GraphDataContext Context { get; set; }

        void Add(T entity);
        void Remove(T entity);
        IQueryable<T> AsQuery();
        T Get(Guid id);
    }

    public abstract class BaseRepository<T> : IRepository<T> where T : DBEntity
    {
        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract IQueryable<T> AsQuery();
        public abstract T Get(Guid id);

        private GraphDataContext _context;

        //TODO: public??? wtf??
        public GraphDataContext Context
        {
            get { return _context ?? (_context = new GraphDataContext(ConfigRepository.ConnectionString())); }
            set { _context = value; }
        }
    }

}