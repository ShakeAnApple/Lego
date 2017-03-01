using System;
using System.Collections.Generic;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    // BL
    public interface IAudioEntryRepository
    {
        List<AudioEntry> GetAll();
        List<AudioEntry> GetByPath(string path);

        void Delete(Guid id);
        void DeleteAll();
    }
}
