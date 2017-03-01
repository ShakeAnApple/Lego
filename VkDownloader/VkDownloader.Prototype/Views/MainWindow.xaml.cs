using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Concrete;
using VkDownloader.Prototype.ViewModels;

namespace VkDownloader.Prototype.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISettingsService _settingsService;
        
        public MainWindow()
        {
            _settingsService = new SettingsService();

            OnStartUp();
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        private void OnStartUp()
        {
            try
            {
                _settingsService.InitializeContext();
            }
            catch (ApplicationException ex)
            {
                // TODO open settings dialog with warning message and Cancel button disabled or nope
                var settingsController = new SettingsController();

                SettingsWindow.ShowSettingsDialog(settingsController.SettingsViewModel);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
