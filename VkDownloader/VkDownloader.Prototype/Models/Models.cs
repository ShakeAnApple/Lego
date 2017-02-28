﻿using System;
using System.IO;

namespace VkDownloader.Prototype
{
    public class AudioBase
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public int Size { get; set; }
    }

    public class AudioFile : AudioBase
    {
        public Stream Content;
    }

    public class AudioEntry : AudioBase
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }
        public DateTime UploadedDate { get; set; }
        public DateTime AddedDate { get; set; }
    }

    public class Context
    {
        private static Context _context;

        public static Context Current
        {
            get
            {
                if (_context == null)
                {
                    _context = new Context();
                }
                return _context;
            }
        }

        public Settings Settings { get; set; }
    }

    public class Settings
    {
        public string AccountId { get; set; }
        public ScanningArea ScanningArea { get; set; }
        public string DefaultDownloadPath { get; set; }
    }

    public enum ScanningArea
    {
        None = 0,
        Audios,
        Wall,
        All
    }
}
