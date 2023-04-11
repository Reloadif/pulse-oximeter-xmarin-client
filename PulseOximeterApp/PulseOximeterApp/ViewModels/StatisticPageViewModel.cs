using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.ViewModels.StatisticTab;
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

            var PulseStatistic = new PulseStatisticPage();
            var PulseStatisticVM = new PulseStatisticPageViewModel();
            PulseStatistic.BindingContext = PulseStatisticVM;
            await Shell.Current.Navigation.PushAsync(PulseStatistic);

            IsActivityIndicator = false;
        }

        public ICommand GoSaturationStatistic { get; private set; }

        private async void ExecuteGoSaturationStatistic(object obj)
        {
            IsActivityIndicator = true;

            var SaturationStatistic = new SaturationStatisticPage();
            var SaturationStatisticVM = new SaturationStatisticPageViewModel();
            SaturationStatistic.BindingContext = SaturationStatisticVM;
            await Shell.Current.Navigation.PushAsync(SaturationStatistic);

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
