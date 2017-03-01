using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Concrete;

namespace VkDownloader.Prototype.ViewModels
{
    public class SettingsController
    {
        private readonly ISettingsService _settingsService;

        public SettingsViewModel SettingsViewModel { get; private set; }

        public SettingsController()
        {
            _settingsService = new SettingsService();

            SettingsViewModel = new SettingsViewModel();
        }

        public void Apply()
        {
            var settingsFilePath = SettingsViewModel.ConfigSettings.TempSettingsFilePath ?? SettingsViewModel.ConfigSettings.DefaultSettingsFilePath;
            if (string.IsNullOrEmpty(settingsFilePath))
            {
                return;
            }

            _settingsService.SaveConfigurationSettings(SettingsViewModel.ConfigSettings);
            _settingsService.Save(SettingsViewModel.Settings, settingsFilePath);
        }

    }
}
