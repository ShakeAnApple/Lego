namespace FileSystemWatcherTest.Repository.Database
{
    public class UnitOfWork
    {
        private GraphDataContext _context;

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

        public Repository<T> GetRepository<T>() where T : DBEntity
        {
            var repo = new Repository<T>();
            repo.Context = _context;
            return repo;
        }

        public void Dispose()
        {
            if (_context == null)
            {
                return;
            }

            _context.Dispose();
            _context = null;
        }
    }
}