using System;
using System.Linq;

namespace FileSystemWatcherTest.Repository.Database
{
    public interface IBaseRepository<T>
    {
        void Save(T entity);
        void Remove(T entity);
        IQueryable<T> AsQuery();
        T Get(Guid id);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        public abstract void Save(T entity);
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