using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.StatisticTab;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels
{
    class StatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private bool _isActivityIndicator;
        #endregion

        #region Properties
        public bool IsActivityIndicator
        {
            get => _isActivityIndicator;
            set => Set(ref _isActivityIndicator, value);
        }
        #endregion

        #region Commands
        public ICommand GoPulseStatistic { get; private set; }

        private async void ExecuteGoPulseStatistic(object obj)
        {
            IsActivityIndicator = true;

            await Shell.Current.Navigation.PushAsync(new PulseStatisticPage());

            IsActivityIndicator = false;
        }

        public ICommand GoSaturationStatistic { get; private set; }

        private async void ExecuteGoSaturationStatistic(object obj)
        {
            IsActivityIndicator = true;

            await Shell.Current.Navigation.PushAsync(new SaturationStatisticPage());

            IsActivityIndicator = false;
        }
        #endregion

        #region Base Methods
        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        #endregion

        public StatisticPageViewModel()
        {
            GoPulseStatistic = new Command(ExecuteGoPulseStatistic);
            GoSaturationStatistic = new Command(ExecuteGoSaturationStatistic);
        }
    }
}
