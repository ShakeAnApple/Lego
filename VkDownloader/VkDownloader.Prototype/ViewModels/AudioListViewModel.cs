using System.Collections.ObjectModel;

namespace VkDownloader.Prototype.ViewModels
{
    public class AudioListViewModel : BaseNotifiableViewModel
    {
        private readonly IAudioEntryRepository _audioEntryRepostory;

        public ObservableCollection<AudioEntryViewModel> Items { get; set; }

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

        public AudioListViewModel()
        {
            _audioEntryRepostory = new AudioEntryRepositoryMock();
            Items = new ObservableCollection<AudioEntryViewModel>();

            ShowDownloadedCommand = new DelegateCommand(obj => ShowDownloaded());
        }

        public DelegateCommand ShowDownloadedCommand { get; private set; }

        public void SetAllChecked(bool value)
        {
            _allChecked = value;
            base.OnPropertyChanged("AllChecked");
        }

        public void ShowDownloaded()
        {
            Items.Clear();

            _audioEntryRepostory
                .List()
                .ForEach(a => Items.Add(new AudioEntryViewModel(this, a)));
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
