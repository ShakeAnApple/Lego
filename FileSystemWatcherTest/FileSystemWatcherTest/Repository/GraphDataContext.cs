using System;
using System.Data.SqlServerCe;
using System.Data.Linq;


namespace FileSystemWatcherTest.Repository
{
    public class GraphDataContext : DataContext
    {
        public Table<Message> Messages;
        public Table<TagType> Tags;
        public Table<MessageTag> TaggedMessages;
        public Table<File> Files;

        public GraphDataContext(string connString) 
            : base(new SqlCeConnection(connString))
        {
        }

        public static void Test()
        {
            var cnnString = "Data Source=MyData.sdf;Persist Security Info=False;";

            using (var cnn = new GraphDataContext(cnnString))
            {
                cnn.CreateDatabase();

                //var s = cnn.Messages.Join(cnn.MessageTags, m => m.Id, k => k.MessageId, (m, k) => new { m, k });
                ////cnn.ExecuteQuery<Message>("select * from messages");

                //foreach (var item in s)
                //{

                //}

                for (int i = 0; i < 10; i++)
                {
                    cnn.Messages.InsertOnSubmit(new Message {Id = Guid.NewGuid(), Body = "olol_" + i});
                }

                cnn.SubmitChanges();
            }
        }
    }
}