//using System;
//using System.Collections.Generic;
//using System.IO;
//using FileSystemWatcherTest.Models;

//namespace FileSystemWatcherTest
//{
//    public class Parser 
//    {
//        public List<LogFile> GetLogs(IEnumerable<string> paths)
//        {
//            var result = new List<LogFile>();
//            foreach (var path in paths)
//            {
//                if (!File.Exists(path))
//                {
//                    continue;
//                }

//                var lines = File.ReadAllLines(path);
//                var logFile = new LogFile
//                {
//                    FullName = path,
//                    Messages = new List<SimpleMessage>()
//                };

//                for (int lineNum = 0; lineNum < lines.Length; lineNum++)
//                {
//                    var line = string.Empty;
//                    // check if it's one-line message
//                    // if not, merge several lines
//                    var message = new SimpleMessage
//                    {
//                        Timestamp = ExtractTimestamp(line),
//                        Thread = ExtractThread(line),
//                        Severity = ExtractStatus(line),
//                        Body = ExtractBody(line)
//                    };
//                }
//            }

//            return result;
//        }

//        private DateTime ExtractTimestamp(string line)
//        {
//            throw new NotImplementedException();
//        }

//        private int ExtractThread(string line)
//        {
//            throw new NotImplementedException();
//        }

//        private Status ExtractStatus(string line)
//        {
//            throw new NotImplementedException();
//        }

//        private string ExtractBody(string line)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}