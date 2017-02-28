using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkDownloader.Prototype.ViewModels
{
    public class SettingsViewModel : BaseNotifiableViewModel
    {
        private readonly ISettingsRepository _settingsRepository;

        private readonly Settings _settings;

        public SettingsViewModel()
        {
            _settingsRepository = new SettingsRepositoryMock();
            _settings = _settingsRepository.Get();
        }

        #region properties
        public string AccountId
        {
            get { return _settings.AccountId; }
            set {
                _settings.AccountId = value;
                OnPropertyChanged("AccountId");
            }
        }
        public ScanningArea ScanningArea
        {
            get { return _settings.ScanningArea; }
            set
            {
                _settings.ScanningArea = value;
                OnPropertyChanged("ScanningArea");
            }
        }
        public string DefaultDownloadPath
        {
            get { return _settings.DefaultDownloadPath; }
            set
            {
                _settings.DefaultDownloadPath = value;
                OnPropertyChanged("DefaultDownloadPath");
            }
        }
        #endregion

        public void Save()
        {
            _settingsRepository.Save();
        }

    }
}
