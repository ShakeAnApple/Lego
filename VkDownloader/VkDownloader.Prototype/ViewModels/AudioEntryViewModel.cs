using System;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.ViewModels
{
    public class AudioEntryViewModel : BaseNotifiableViewModel
    {
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
            base.OnPropertyChanged("IsChecked");
        }
    }
}
