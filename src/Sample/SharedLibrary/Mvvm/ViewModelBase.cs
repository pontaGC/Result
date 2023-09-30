using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SharedLibrary.Mvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <inheritdoc />
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add { this.propertyChanged += value; }
            remove { this.propertyChanged -= value; }
        }

        private event PropertyChangedEventHandler? propertyChanged;

        protected bool SetPropertyValue<T>(ref T backingField, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, newValue))
            {
                return false;
            }

            backingField = newValue;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyname));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.propertyChanged?.Invoke(this, e);
        }
    }
}