using System.Configuration;
using VkDownloader.Prototype.BusinessLogic.Abstract;

namespace VkDownloader.Prototype.BusinessLogic.Concrete
{
    class ApplicationConfigurationRepository : IApplicationConfigurationRepository
    {
        public string GetDefaultSettingsPath()
        {
            return ConfigurationManager.AppSettings["defaultSettingsPath"].ToString();
        }
    }
}
