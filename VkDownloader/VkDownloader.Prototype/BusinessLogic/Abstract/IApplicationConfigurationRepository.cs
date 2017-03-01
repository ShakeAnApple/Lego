namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    // BL
    public interface IApplicationConfigurationRepository
    {
        //var defaultSettingsPath = ConfigurationManager.AppSettings["defaultSettingsPath"].ToString();
        string GetDefaultSettingsPath();
        void UpdateDefaultSettingsPath(string path);
    }
}
