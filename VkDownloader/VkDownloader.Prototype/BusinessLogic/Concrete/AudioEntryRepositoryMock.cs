using System;
using System.Collections.Generic;
using System.IO;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Concrete
{
    public class AudioEntryRepositoryMock : IAudioEntryRepository
    {
        public List<AudioEntry> List()
        {
            return new List<AudioEntry>
            {
                new AudioEntry {
                    Artist = "Artist1",
                    Title = "Title1",
                    Length = 1,
                    AddedDate = DateTime.Now,
                    UploadedDate = DateTime.UtcNow,
                    FilePath = Directory.GetCurrentDirectory()
                },
                new AudioEntry {
                    Artist = "Artist2",
                    Title = "Title2",
                    Length = 2,
                    AddedDate = DateTime.Now,
                    UploadedDate = DateTime.UtcNow,
                    FilePath = Directory.GetCurrentDirectory()
                },
                new AudioEntry {
                    Artist = "Artist3",
                    Title = "Title3",
                    Length = 3,
                    AddedDate = DateTime.Now,
                    UploadedDate = DateTime.UtcNow,
                    FilePath = Directory.GetCurrentDirectory()
                },
            };
        }
    }
}
