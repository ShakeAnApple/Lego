using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VkDownloader.Prototype.ViewModels;

namespace VkDownloader.Prototype.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        public static bool ShowSettingsDialog(SettingsViewModel viewContext)
        {
            var wnd = new SettingsWindow();
            wnd.DataContext = viewContext;

            return wnd.ShowDialog() == true;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnBrowseTempSettings_Click(object sender, RoutedEventArgs e)
        {
            var fileName = ShowSettingsSearchFileDialog();
            this.TemporarySettingsFilePathTextBox.Text = fileName;
        }

        private void btnBrowseDefaultSettings_Click(object sender, RoutedEventArgs e)
        {
            var fileName = ShowSettingsSearchFileDialog();
            this.DefaultSettingsPathTextBox.Text = fileName;
        }

        private string ShowSettingsSearchFileDialog()
        {
            var openFileDialog = new OpenFileDialog();
            // TODO add filters

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        private void btnBrowseDownloadPath_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.DownloadPathTextBox.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
