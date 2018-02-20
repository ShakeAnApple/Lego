using System;
using System.Collections.Generic;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Concrete
{
    class AudioService : IAudioService
    {
        public void ClearHistory()
        {
            throw new NotImplementedException();
        }

        public void Download(List<Guid> audios)
        {
            throw new NotImplementedException();
        }

        public void Download(AudioEntryBL audio)
        {
            throw new NotImplementedException();
        }

        public List<AudioEntryBL> Get()
        {
            throw new NotImplementedException();
        }

        public List<AudioEntryBL> ListDownloaded()
        {
            throw new NotImplementedException();
        }

        public List<AudioEntryBL> ListDownloaded(Period period)
        {
            throw new NotImplementedException();
        }

        public void UpdatePath(Guid trackId, string newPath)
        {
            throw new NotImplementedException();
        }
    }
}
