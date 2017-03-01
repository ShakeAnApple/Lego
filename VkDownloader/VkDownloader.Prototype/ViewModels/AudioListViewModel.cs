using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Concrete;

namespace VkDownloader.Prototype.ViewModels
{
    public class AudioListViewModel : BaseNotifiableViewModel
    {
        private readonly IAudioService _audioService;

        public ObservableCollection<AudioEntryViewModel> Items { get; set; }

        public ICommand GetCommand { get; private set; }
        public ICommand DownloadCheckedCommand { get; private set; }
        public ICommand ShowDownloadedCommand { get; private set; }

        public AudioListViewModel()
        {
            _audioService = new AudioService();
            Items = new ObservableCollection<AudioEntryViewModel>();

            GetCommand = new DelegateCommand(obj => Get());
            DownloadCheckedCommand = new DelegateCommand(obj => DownloadChecked());
            ShowDownloadedCommand = new DelegateCommand(obj => ShowDownloaded());
        }

        private bool _allChecked;
        public bool AllChecked
        {
            get { return _allChecked; }
            set
            {
                this.CheckAll(value);
                this.SetAllChecked(value);
            }
        }
        
        public void ShowDownloaded()
        {
            Items.Clear();

            _audioService
                .ListDownloaded()
                .ForEach(a => Items.Add(new AudioEntryViewModel(this, a)));
        }

        private void DownloadChecked()
        {
            var checkedTracks = Items.Where(item => item.IsChecked)
                .Select(item => item.Id)
                .ToList();
            _audioService.Download(checkedTracks);
        }

        private void Get()
        {
            Items.Clear();

            _audioService
                .Get()
                .ForEach(a => Items.Add(new AudioEntryViewModel(this, a)));
        }

        public void SetAllChecked(bool value)
        {
            _allChecked = value;
            base.OnPropertyChanged("AllChecked");
        }

        private void CheckAll(bool value)
        {
            foreach (var item in Items)
            {
                item.SetChecked(value);
            }
        }        
    }
}
