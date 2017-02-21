using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VkDownloader.Prototype.ViewModels
{
    public abstract class BaseNotifiableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
