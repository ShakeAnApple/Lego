using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemWatcherTest.Old
{
    // TODO just to test, replace this
    public class LogLine
    {
        public string Date { get; set; }
        public int Thread { get; set; }
        public LogMessageStatus Status { get; set; }
        public string Message { get; set; }
    }

    public enum LogMessageStatus
    {
        Info,
        Warning,
        Error
    }

    public class FileContentPreprocessor
    {
        private FileContentPreprocessor()
        {
        }

        private string ExtractTimestamp(string line, ref int pos)
        {
            if (line[pos] != '[')
            {
                throw new ArgumentException("Unsupported timestamp format");
            }
            var startPos = ++pos;
            var length = 0;
            while (line[pos] != ']')
            {
                pos++;
                length++;
            }
            pos ++;
            return line.Substring(startPos, length);
        }

        private int ExtractThread(string line, ref int pos)
        {
            if (line[pos] != '<')
            {
                throw new ArgumentException("Unsupported thread format");
            }
            var startPos = ++pos;
            var length = 0;
            while (line[pos] != '>')
            {
                pos++;
                length++;
            }
            pos++;
            return int.Parse(line.Substring(startPos, length));
        }

        private LogMessageStatus ExtractStatus(string line, ref int pos)
        {
            var startPos = pos;
            var length = 0;
            while (!char.IsWhiteSpace(line[pos]))
            {
                pos++;
                length++;
            }
            pos++;
            var status = line.Substring(startPos, length);
            switch (status.ToLower())
            {
                case "info":
                    return LogMessageStatus.Info;
                case "warning":
                    return LogMessageStatus.Warning;
                case "error":
                    return LogMessageStatus.Error;
                default:
                    throw new ArgumentException("Not a status");
            }
        }

        public List<LogLine> GetLogLinesInternal(string filePath)
        {
            var result = new List<LogLine>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line) || line[0] != '[')
                {
                    continue;
                }

                var pos = 0;

                var logLine = new LogLine();
                logLine.Date = this.ExtractTimestamp(line, ref pos);
                pos++;
                logLine.Thread = this.ExtractThread(line, ref pos);
                pos++;
                logLine.Status = this.ExtractStatus(line, ref pos);
                pos++;
                logLine.Message = line.Substring(pos + 1);

                result.Add(logLine);
            }
            
            return result;
        }

        public static List<LogLine> GetLogLines(string path)
        {
            return new FileContentPreprocessor().GetLogLinesInternal(path);
        }
    }
}