namespace VkDownloader.Prototype.BusinessLogic.Model
{
    public interface IContext
    {
        Settings Settings { get; }
        string TemporarySettingsPath { get; set; }

        void Update(Settings settings);
    }
}
