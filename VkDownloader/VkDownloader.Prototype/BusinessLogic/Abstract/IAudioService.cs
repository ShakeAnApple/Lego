using System;
using System.Collections.Generic;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    public interface IAudioService
    {
        List<AudioEntryBL> Get();

        List<AudioEntryBL> ListDownloaded();
        List<AudioEntryBL> ListDownloaded(Period period);

        void Download(AudioEntryBL audio);
        void Download(List<Guid> audiosId);

        void UpdatePath(Guid trackId, string newPath);
        void ClearHistory();
    }
}
