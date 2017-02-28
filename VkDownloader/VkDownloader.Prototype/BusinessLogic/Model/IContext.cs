namespace VkDownloader.Prototype.BusinessLogic.Model
{
    public interface IContext
    {
        Settings Settings { get; }

        void Update(Settings settings);
    }
}
