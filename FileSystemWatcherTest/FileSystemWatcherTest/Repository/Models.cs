using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace FileSystemWatcherTest.Repository
{
    [Table]
    public class File
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public string FullName { get; set; }

        [Association(ThisKey="Id", OtherKey = "FileId")]
        public EntitySet<Message> Messages { get; set; }
    }

    [Table]
    public class Message
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public Guid FileId { get; set; }

        [Column]
        public int Order { get; set; }

        [Column]
        public string Body { get; set; }
    }

    [Table]
    public class MessageTag
    {
        [Column]
        public Guid MessageId { get; set; }

        [Column]
        public Guid TagId { get; set; }

        [Column]
        public int StartPos { get; set; }

        [Column]
        public int Length { get; set; }
    }

    [Table]
    public class Tag
    {
        [Column]
        public Guid TagId { get; set; }

        [Column]
        public string Value { get; set; }
    }

    [Table]
    public class TagType
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public string Name { get; set; }
    }

}