using System.Configuration;
using VkDownloader.Prototype.BusinessLogic.Abstract;

namespace VkDownloader.Prototype.BusinessLogic.Concrete
{
    class ApplicationConfigurationRepository : IApplicationConfigurationRepository
    {
        private const string DefaultSettingsPathKey = "defaultSettingsPath";
        private const string AppAettingsSection = "appSettings";

        public string GetDefaultSettingsPath()
        {
            return ConfigurationManager.AppSettings[DefaultSettingsPathKey].ToString();
        }

        public void UpdateDefaultSettingsPath(string path)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[DefaultSettingsPathKey].Value = path;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(AppAettingsSection);
        }
    }
}
