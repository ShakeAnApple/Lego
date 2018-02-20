using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    // BL
    public interface ISettingsService
    {
        Settings Get(string path);
        Settings GetCurrent();
        Settings GetDefault();
        ConfigurationSettings GetConfigurationSettings();

        void InitializeContext();
        void UpdateContextWithTempSettings(string path);

        void Save(Settings settings, string path);
        void SaveConfigurationSettings(ConfigurationSettings settings);
    }
}
