using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    public interface ISettingsService
    {
        void Load(string path);
        void InitializeContext();
        void Save(Settings settings, string path);
    }
}
