namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    // BL
    public interface IApplicationConfigurationRepository
    {
        string GetDefaultSettingsPath();
        void UpdateDefaultSettingsPath(string path);
    }
}
