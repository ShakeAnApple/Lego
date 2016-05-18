using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace FileSystemWatcherTest.Repository.Database
{

    public class DBEntity
    {
        public Guid Id { get; set; }
    }

    [Table]
    public class File : DBEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column(CanBeNull = false)]
        public string FullName { get; set; }

        [Association(ThisKey="Id", OtherKey = "FileId")]
        public EntitySet<Message> Messages { get; set; }
    }

    [Table]
    public class Message : DBEntity
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
    public class MessageTag : DBEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

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
    public class Tag : DBEntity
    {
        [Column]
        public Guid Id { get; set; }

        [Column]
        public Guid TagTypeId { get; set; }

        [Column]
        public string Value { get; set; }
    }

    [Table]
    public class TagType : DBEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column(CanBeNull = false)]
        public string Name { get; set; }

        [Column(CanBeNull = false)]
        public string Pattern { get; set; }
    }

}