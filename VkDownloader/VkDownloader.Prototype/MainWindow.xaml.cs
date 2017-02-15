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

namespace VkDownloader.Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<AudioEntry> audios { get; private set; }
        private readonly IAudioEntryRepository _audioEntryRepostory;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _audioEntryRepostory = new AudioEntryRepositoryMock();
            audios = _audioEntryRepostory.List();
        }
    }
}
