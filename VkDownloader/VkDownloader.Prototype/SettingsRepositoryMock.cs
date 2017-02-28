using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkDownloader.Prototype
{
    public class SettingsRepositoryMock : ISettingsRepository
    {
        public Settings Get()
        {
            return new Settings
            {
                AccountId = "id199989126",
                DefaultDownloadPath = Directory.GetCurrentDirectory(),
                ScanningArea = ScanningArea.Audios
            };
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
