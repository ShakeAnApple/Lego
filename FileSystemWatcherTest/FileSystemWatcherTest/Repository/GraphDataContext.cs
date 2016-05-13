using System.Data.SqlServerCe;
using System.Data.Linq;


namespace FileSystemWatcherTest.Repository
{
    public class GraphDataContext : DataContext
    {
        public Table<Message> Messages;
        public Table<Tag> Tags;
        public Table<TaggedMessage> TaggedMessages;

        public GraphDataContext(string connString) 
            : base(new SqlCeConnection(connString))
        {
        }
    }
}