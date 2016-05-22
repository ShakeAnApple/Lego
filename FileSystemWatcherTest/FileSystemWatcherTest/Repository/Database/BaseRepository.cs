using System;
using System.Linq;

namespace FileSystemWatcherTest.Repository.Database
{
    public interface IRepository<T> where T : IDbEntity
    {
        GraphDataContext Context { get; }

        void Add(T entity);
        void Remove(T entity);
        IQueryable<T> AsQuery();
        T Get(Guid id);
    }

    public abstract class BaseRepository<T> : IRepository<T> where T : IDbEntity
    {
        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract IQueryable<T> AsQuery();
        public abstract T Get(Guid id);

        private readonly GraphDataContext _context;

        public BaseRepository(GraphDataContext context)
        {
            _context = context;
        }

        public GraphDataContext Context
        {
            get { return _context; }
        }
    }

}