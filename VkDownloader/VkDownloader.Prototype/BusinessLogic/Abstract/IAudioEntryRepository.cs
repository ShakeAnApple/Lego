using System.Collections.Generic;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    public interface IAudioEntryRepository
    {
        List<AudioEntry> List();
        //void Clear();
        //void Delete();
    }
}
