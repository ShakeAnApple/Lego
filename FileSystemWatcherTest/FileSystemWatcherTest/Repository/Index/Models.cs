using System;
using System.Collections.Generic;

namespace FileSystemWatcherTest.Repository.Index
{
    public class File
    {
        //Use it as id
        public string Path { get; set; }
        public List<Message> Content { get; set; }
    }

    public enum Status
    {
        None,
        Info,
        Warning,
        Error,
        Critical
    }

    public class Message
    {
        public Guid Id { get; private set; }

        public Message()
        {
            this.Id = Guid.NewGuid();
        }

        public string Body { get; set; }
        public int Thread { get; set; }
        public DateTime Timestamp { get; set; }
        public Status Severity { get; set; }
    }
}