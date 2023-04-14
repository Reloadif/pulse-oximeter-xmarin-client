using PulseOximeterApp.Infrastructure.DependencyServices;
using PulseOximeterApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.SettingTab
{
    class OptionPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly int _oldNumberOfPulseMeasure;
        private readonly int _oldNumberOfOxigenMeasure;

        private int _numberOfPulseMeasure;
        private int _numberOfOxigenMeasure;
        #endregion

        #region Properties
        public int NumberOfPulseMeasure
        {
            get => _numberOfPulseMeasure;
            set => Set(ref _numberOfPulseMeasure, value);
        }

        public int NumberOfOxigenMeasure
        {
            get => _numberOfOxigenMeasure;
            set => Set(ref _numberOfOxigenMeasure, value);
        }
        #endregion

        #region Commands
        #endregion

        #region Base Methods
        public override void OnAppearing()
        {
            base.OnAppearing();

            (Application.Current.MainPage.BindingContext as MainPageViewModel).HomeTabIsEnabled = false;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = false;
        }

        public async override void OnDisappearing()
        {
            if (_oldNumberOfPulseMeasure != _numberOfPulseMeasure || _oldNumberOfOxigenMeasure != _numberOfOxigenMeasure)
            {
                if (await DependencyService.Get<IShowMessageDependencyService>().ShowQuestionAsync("Подтвердить действие", "Сохранить изменения?"))
                {
                    Preferences.Set("NumberOfPulseMeasure", _numberOfPulseMeasure);
                    Preferences.Set("NumberOfOxigenMeasure", _numberOfOxigenMeasure);
                }
            }

            (Application.Current.MainPage.BindingContext as MainPageViewModel).HomeTabIsEnabled = true;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = true;

            base.OnDisappearing();
        }
        #endregion

        public OptionPageViewModel()
        {
            NumberOfPulseMeasure = Preferences.Get("NumberOfPulseMeasure", 30);
            NumberOfOxigenMeasure = Preferences.Get("NumberOfOxigenMeasure", 30);

            _oldNumberOfPulseMeasure = _numberOfPulseMeasure;
            _oldNumberOfOxigenMeasure = _numberOfOxigenMeasure;
        }
    }
}
