using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FileSystemWatcherTest
{
    public class File
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }

        public File(Guid id, string fullName)
        {
            this.Id = id;
            this.FullName = fullName;
        }
    }

    public class Message
    {
        public Guid Id { get; private set; }
        public Guid FileId { get; private set; }

        public Message(Guid id, Guid fileId)
        {
            this.Id = id;
            this.FileId = fileId;
        }

        public string Body { get; set; }
        public List<MessageTag> Tags { get; set; }
    }

    public class MessageTag
    {
        public Tag Tag { get; set; }
        public string Value { get; set; }
    }

    public class Tag
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Pattern { get; private set; }

        public Tag(Guid id, string name, string pattern)
        {
            this.Id = id;
            this.Name = name;
            this.Pattern = pattern;
        }

        private Regex _regex;

        public Regex GetRegex()
        {
            return _regex ?? (_regex = new Regex(Pattern));
        }
    }

    public enum Status
    {
        None,
        Info,
        Warning,
        Error,
        Critical
    }
}