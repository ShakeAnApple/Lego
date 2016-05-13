using System;
using System.Collections.Generic;

namespace FileSystemWatcherTest.Models
{
    public class LogFile
    {
        public string FullName { get; set; }
        public List<SimpleMessage> Messages { get; set; }
    }

    public class LogMessage
    {
        public DateTime Timestamp { get; set; }
        public Status Severity { get; set; }
        public int Thread { get; set; }
    }

    public class SimpleMessage : LogMessage
    {
        public string Body { get; set; }
    }

    public class TaggedMessage : LogMessage
    {
        public List<Tag> Content { get; set; }
    }

    public enum Status
    {
        None,
        Info,
        Warning,
        Error,
        Critical
    }
    
    public class Tag
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}