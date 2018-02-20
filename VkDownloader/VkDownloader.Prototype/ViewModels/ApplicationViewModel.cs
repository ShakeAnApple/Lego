using System.Windows.Input;
using VkDownloader.Prototype.Views;

namespace VkDownloader.Prototype.ViewModels
{
    public class ApplicationViewModel
    {
        public AudioListViewModel AudioList { get; private set; }

        public ICommand EditSettingsCommand { get; private set; }

        public ApplicationViewModel()
        {
            AudioList = new AudioListViewModel();

            EditSettingsCommand = new DelegateCommand(obj => EditSettings());
        }
               

        private void EditSettings()
        {
            var settingsController = new SettingsController();

            var result = SettingsWindow.ShowSettingsDialog(settingsController.SettingsViewModel);
            if (result)
            {
                settingsController.Apply();
            }
        }
    }    
}
