using System.Data.Linq;
using System.Data.SqlServerCe;

namespace FileSystemWatcherTest.Repository.Database
{
    public class GraphDataContext : DataContext
    {
        public Table<Message> Messages;
        public Table<TagType> TagTypes;
        public Table<Tag> Tags;
        public Table<MessageTag> MessageTags;
        public Table<File> Files;

        public GraphDataContext(string connString) 
            : base(new SqlCeConnection(connString))
        {
        }

        //public static void Test()
        //{
            
            //var cnnString = "Data Source=MyData.sdf;Persist Security Info=False;";

            //using (var cnn = new GraphDataContext(cnnString))
            //{
            //    cnn.CreateDatabase();

            //    //var s = cnn.Messages.Join(cnn.MessageTags, m => m.Id, k => k.MessageId, (m, k) => new { m, k });
            //    ////cnn.ExecuteQuery<Message>("select * from messages");

            //    //foreach (var item in s)
            //    //{

            //    //}
            //    var fileId = Guid.NewGuid();
            //    var file = new File {FullName = "kpk", Id = fileId, Messages = new EntitySet<Message>()};
            //    cnn.Files.InsertOnSubmit(file);
            //    for (int i = 0; i < 10; i++)
            //    {
            //        var messId = Guid.NewGuid();
            //        cnn.Messages.InsertOnSubmit(new Message {Id = messId, Body = "olol_" + i, FileId = fileId});
            //    }
                
            //    cnn.SubmitChanges();

            //    var dbFile = cnn.Files.First(f => f.Id == fileId);
            //    foreach (var message in dbFile.Messages)
            //    {
            //        Console.WriteLine(message.Id + " " + message.Body);
            //    }
            //}
        //}
    }
}