using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FileSystemWatcherTest.Repository
{
    public interface IBaseRepository<T>
    {
        void Save(T entity);
        void Remove(Guid id);
        List<T> List();
        T Get(Guid id);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        public abstract void Save(T entity);
        public abstract void Remove(Guid id);
        public abstract List<T> List();
        public abstract T Get(Guid id);

        private GraphDataContext _context;

        protected GraphDataContext GetContext()
        {
            return _context ?? (_context = new GraphDataContext(ConfigRepository.ConnectionString()));
        }
    }

    public class LolRepo : BaseRepository<File>
    {
        public override void Save(File entity)
        {
            var context = GetContext();
            context.Files.InsertOnSubmit(entity);
            context.SubmitChanges();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override List<File> List()
        {
            throw new NotImplementedException();
        }

        public override File Get(Guid id)
        {
            var context = GetContext();
            return context.Files.FirstOrDefault(f => f.Id == id);
        }
    }

}