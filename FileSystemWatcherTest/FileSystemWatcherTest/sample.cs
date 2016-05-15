using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace FileSystemWatcherTest
{
    public class MyDataContext : DataContext
    {
        public Table<Message> Messages { get { return base.GetTable<Message>(); } }
        public Table<MessageTag> MessageTags { get { return base.GetTable<MessageTag>(); } }
        public Table<TagKind> TagKinds { get { return base.GetTable<TagKind>(); } }

        public MyDataContext(string cnnString)
            : base(new SqlCeConnection(cnnString))
        {

        }

    }

    [Table]
    public class SourceFile
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public string Path { get; set; }
    }

    [Table]
    public class Message
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public ulong Order { get; set; }

        [Column]
        public string Text { get; set; }

        [Column]
        public Guid SourceId { get; set; }
    }

    [Table]
    public class MessageTagEntry
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public Guid MessageId { get; set; }

        [Column]
        public Guid MessageTagId { get; set; }

        [Column]
        public int PositionInText { get; set; }
    }

    [Table]
    public class MessageTag
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public Guid TagKindId { get; set; }

        [Column]
        public string Value { get; set; }
    }

    [Table]
    public class TagKind
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public string Name { get; set; }
    }
}