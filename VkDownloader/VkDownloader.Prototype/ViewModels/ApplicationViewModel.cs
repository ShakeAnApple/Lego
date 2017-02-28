using System;
using System.Windows.Input;
using VkDownloader.Prototype.Views;

namespace VkDownloader.Prototype.ViewModels
{
    public class ApplicationViewModel
    {
        public AudioListViewModel AudioList { get; private set; }

        public ICommand ShowSettingsWindowCommand { get; private set; }

        public ApplicationViewModel()
        {
            AudioList = new AudioListViewModel();

            ShowSettingsWindowCommand = new DelegateCommand(obj => ShowSettingsWindow());
        }
               

        private void ShowSettingsWindow()
        {
            if (SettingsWindow.ShowSettingsDialog().Value)
            {
                //context.applyNewSettings
            }
        }
    }    
}
