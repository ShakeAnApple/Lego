using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using FileSystemWatcherTest.Models;

namespace FileSystemWatcherTest.Repository
{
    [Table]
    public class File
    {
        private Guid _id;

        [Column(Storage="_id", IsPrimaryKey = true, IsDbGenerated = true)]
        public Guid Id { get { return this._id; } }

        public string FullName { get; set; }

        [Association(ThisKey="Id", OtherKey = "FileId")]
        public EntitySet<Message> Messages { get; set; }
    }

    [Table]
    public class Message
    {
        private Guid _id;

        [Column(Storage = "_id", IsPrimaryKey = true, IsDbGenerated = true)]
        public Guid Id {get { return this._id; } }

        [Association(IsForeignKey = true)]
        public Guid FileId { get; set; }

        public DateTime Timestamp { get; set; }
        public int Order { get; set; }
        public Status Severity { get; set; }
        public int Thread { get; set; }
        public string Body { get; set; }
    }

    [Table]
    public class TaggedMessage
    {
        public Guid MessageId { get; set; }
        public Guid TagId { get; set; }

        public string TagValue { get; set; }
    }

    [Table]
    public class Tag
    {
        private Guid _id;

        [Column(Storage = "_id", IsPrimaryKey = true, IsDbGenerated = true)]
        public Guid Id { get { return this._id; } }

        public string Name { get; set; }
    }

}