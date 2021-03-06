﻿using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace FileSystemWatcherTest.Repository.Database
{

    public interface IDbEntity
    {
        Guid Id { get; set; }
    }

    //[Table(Name = "Files")]
    //public class File : IDbEntity
    //{
    //    [Column(IsPrimaryKey = true)]
    //    public Guid Id { get; set; }

    //    [Column(CanBeNull = false)]
    //    public string FullName { get; set; }
    //}

    [Table]
    public class File : IDbEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column(CanBeNull = false)]
        public string FullName { get; set; }

        [Association(ThisKey="Id", OtherKey = "FileId")]
        public EntitySet<Message> Messages { get; set; }
    }

    [Table]
    public class Message : IDbEntity
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
    public class MessageTag : IDbEntity
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
    public class Tag : IDbEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public Guid TagTypeId { get; set; }

        [Column]
        public string Value { get; set; }
    }

    [Table]
    public class TagType : IDbEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column(CanBeNull = false)]
        public string Name { get; set; }

        [Column(CanBeNull = false)]
        public string Pattern { get; set; }
    }
}