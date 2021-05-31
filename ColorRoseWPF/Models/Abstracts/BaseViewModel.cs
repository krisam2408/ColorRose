using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ColorRoseWPF.Models.Abstracts
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isEnabled;
        public bool IsEnabled { get { return isEnabled; } set { SetValue(ref isEnabled, value); } }

        protected BaseViewModel() { }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
