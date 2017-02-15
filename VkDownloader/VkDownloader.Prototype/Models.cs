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

    public enum ScanningArea
    {
        None = 0,
        Audios,
        Wall
    }
}
