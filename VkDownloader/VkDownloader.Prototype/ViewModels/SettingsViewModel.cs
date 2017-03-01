using System.Windows.Input;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Concrete;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.ViewModels
{
    public class SettingsViewModel : BaseNotifiableViewModel
    {
        private readonly ISettingsService _settingsService;
        // TODO nya?
        private readonly SettingsController _owner;

        public Settings Settings { get; private set; }
        public ConfigurationSettings ConfigSettings { get; private set; }

        public ICommand LoadTempSettingsCommand { get; private set; }
        public ICommand SetDefaultSettingsCommand { get; private set; }

        public SettingsViewModel()
        {
            _settingsService = new SettingsService();

            Settings = _settingsService.GetCurrent();
            ConfigSettings = _settingsService.GetConfigurationSettings();

            LoadTempSettingsCommand = new DelegateCommand(o => LoadTempSettings());
            SetDefaultSettingsCommand = new DelegateCommand(o => SetDefaultSettings());
        }

        #region bl settings
        public string AccountId
        {
            get { return Settings.AccountId; }
            set
            {
                Settings.AccountId = value;
                OnPropertyChanged("AccountId");
            }
        }
        public ScanningArea ScanningArea
        {
            get { return Settings.ScanningArea; }
            set
            {
                Settings.ScanningArea = value;
                OnPropertyChanged("ScanningArea");
            }
        }
        public string DefaultDownloadPath
        {
            get { return Settings.DefaultDownloadPath; }
            set
            {
                Settings.DefaultDownloadPath = value;
                OnPropertyChanged("DefaultDownloadPath");
            }
        }
        #endregion

        #region app settings
        public string DefaultSettingsFilePath
        {
            get { return ConfigSettings.DefaultSettingsFilePath; }
            set
            {
                ConfigSettings.DefaultSettingsFilePath = value;
                OnPropertyChanged("DefaultSettingsFilePath");
            }
        }

        public string TempSettingsFilePath
        {
            get { return ConfigSettings.TempSettingsFilePath; }
            set
            {
                ConfigSettings.TempSettingsFilePath = value;
                OnPropertyChanged("TempSettingsFilePath");
            }
        }
        #endregion

        private void LoadTempSettings()
        {
            if (string.IsNullOrEmpty(ConfigSettings.TempSettingsFilePath))
            {
                return;
            }

            var tempSettings = _settingsService.Get(ConfigSettings.TempSettingsFilePath);
            UpdateSettingsView(tempSettings);
        }

        private void SetDefaultSettings()
        {
            var settings = _settingsService.GetDefault();
            var configurationSettings = _settingsService.GetConfigurationSettings();

            UpdateSettingsView(settings);
            ConfigSettings.TempSettingsFilePath = configurationSettings.TempSettingsFilePath;
            ConfigSettings.DefaultSettingsFilePath = configurationSettings.DefaultSettingsFilePath;
        }

        private void UpdateSettingsView(Settings settings)
        {
            Settings.AccountId = settings.AccountId;
            Settings.DefaultDownloadPath = settings.DefaultDownloadPath;
            Settings.ScanningArea = settings.ScanningArea;
        }
    }
}
