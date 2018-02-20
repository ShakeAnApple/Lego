using System;
using System.IO;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Concrete
{
    public class SettingsRepositoryMock : ISettingsRepository
    {
        public Settings Get(string path)
        {
            //return null;
            return new Settings
            {
                AccountId = "id199989126",
                DefaultDownloadPath = Directory.GetCurrentDirectory(),
                ScanningArea = ScanningArea.Audio
            };
        }

        public void Save(Settings settings, string path)
        {
            throw new NotImplementedException();
        }
    }
}
