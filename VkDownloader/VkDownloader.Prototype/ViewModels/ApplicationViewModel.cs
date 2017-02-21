namespace VkDownloader.Prototype.ViewModels
{
    public class ApplicationViewModel
    {
        public AudioListViewModel AudioList { get; private set; }

        public ApplicationViewModel()
        {
            AudioList = new AudioListViewModel();
        }
    }
}
