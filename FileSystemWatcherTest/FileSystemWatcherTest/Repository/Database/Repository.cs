using System;
using System.Linq;

namespace FileSystemWatcherTest.Repository.Database
{
    public class Repository<T> : BaseRepository<T> where T : class, IDbEntity
    {
        public Repository(GraphDataContext context)
            : base(context)
        {
            
        }

        public override void Add(T entity)
        {
            base.Context.GetTable<T>().InsertOnSubmit(entity);
        }

        public override void Remove(T entity)
        {
            base.Context.GetTable<T>().DeleteOnSubmit(entity);
        }

        public override IQueryable<T> AsQuery()
        {
            return base.Context.GetTable<T>();
        }

        public override T Get(Guid id)
        {
            return base.Context.GetTable<T>().FirstOrDefault(entity => entity.Id == id);
        }
    }

}