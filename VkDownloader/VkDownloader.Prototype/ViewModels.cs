using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace VkDownloader.Prototype
{
    public class AudioEntryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly AudioEntry _audio;
        readonly AudioListViewModel _owner;

        public AudioEntryViewModel(AudioListViewModel owner, AudioEntry audio)
        {
            _audio = audio;
            _owner = owner;
        }

        bool _isChecked = false;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (!value)
                    _owner.SetAllChecked(false);

                this.SetChecked(value);
            }
        }

        public string Artist
        {
            get { return _audio.Artist; }
            set { _audio.Artist = value; }
        }
        public string Title
        {
            get { return _audio.Title; }
            set { _audio.Title = value; }
        }
        public int Length
        {
            get { return _audio.Length; }
        }
        public DateTime UploadedDate
        {
            get { return _audio.UploadedDate; }
        }
        public DateTime AddedDate
        {
            get { return _audio.UploadedDate; }
        }
        public string FilePath
        {
            get { return _audio.FilePath; }
        }

        internal void SetChecked(bool value)
        {
            _isChecked = value;
            this.OnPropertyChanged("IsChecked");
        }

        private void OnPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class ApplicationViewModel
    {
        public AudioListViewModel AudioList { get; private set; }

        public ApplicationViewModel()
        {
            AudioList = new AudioListViewModel();
        }
    }

    public class AudioListViewModel : INotifyPropertyChanged
    {
        private readonly IAudioEntryRepository _audioEntryRepostory;

        public event PropertyChangedEventHandler PropertyChanged;

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
            OnPropertyChanged("AllChecked");
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

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
