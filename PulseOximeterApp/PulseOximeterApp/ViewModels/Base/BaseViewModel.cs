using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected Task<T> BeginInvokeOnMainThreadAsync<T>(Func<T> a)
        {
            TaskCompletionSource<T> task = new TaskCompletionSource<T>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    T result = a();
                    task.SetResult(result);
                }
                catch (Exception e)
                {
                    task.SetException(e);
                }
            });

            return task.Task;
        }

        public virtual void OnAppearing()
        {
            // No default implementation. 
        }
        public virtual void OnDisappearing()
        {
            // No default implementation. 
        }
    }
}
