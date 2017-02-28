namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    public interface IApplicationConfigurationRepository
    {
        //var defaultSettingsPath = ConfigurationManager.AppSettings["defaultSettingsPath"].ToString();
        string GetDefaultSettingsPath();
    }
}
