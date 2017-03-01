using System.Collections.Generic;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    // BL
    public interface IAudioEntryRepository
    {
        List<AudioEntry> List();
        //void Clear();
        //void Delete();
    }
}
