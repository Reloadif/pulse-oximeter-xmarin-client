using PulseOximeterApp.Infrastructure.DependencyServices;
using PulseOximeterApp.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.SettingTab
{
    class OptionPageViewModel : BaseViewModel
    {
        #region Fields
        private int _oldNumberOfPulseMeasure;
        private int _oldNumberOfOxigenMeasure;

        private int _numberOfPulseMeasure;
        private int _numberOfOxigenMeasure;
        #endregion

        #region Properties
        public int NumberOfPulseMeasure
        {
            get => _numberOfPulseMeasure;
            set
            {
                if (Set(ref _numberOfPulseMeasure, value))
                {
                    (ResetNumberMeasures as Command)?.ChangeCanExecute();
                    (SaveNumberMeasures as Command)?.ChangeCanExecute();
                }
            }
        }

        public int NumberOfOxigenMeasure
        {
            get => _numberOfOxigenMeasure;
            set
            {
                if (Set(ref _numberOfOxigenMeasure, value))
                {
                    (ResetNumberMeasures as Command)?.ChangeCanExecute();
                    (SaveNumberMeasures as Command)?.ChangeCanExecute();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand ResetNumberMeasures { get; private set; }
        private async void ExecuteResetNumberMeasures(object obj)
        {
            if (await DependencyService.Get<IShowMessageDependencyService>().ShowQuestionAsync("Подтвердить действие", "Сбросить число измерений?"))
            {
                NumberOfPulseMeasure = 30;
                NumberOfOxigenMeasure = 30;
            }
        }
        private bool CanResetNumberMeasures(object obj)
        {
            return _numberOfPulseMeasure != 30 || _numberOfOxigenMeasure != 30;
        }

        public ICommand SaveNumberMeasures { get; private set; }
        private void ExecuteSaveNumberMeasures(object obj)
        {
            _oldNumberOfPulseMeasure = _numberOfPulseMeasure;
            _oldNumberOfOxigenMeasure = _numberOfOxigenMeasure;

            Preferences.Set("NumberOfPulseMeasure", _numberOfPulseMeasure);
            Preferences.Set("NumberOfOxigenMeasure", _numberOfOxigenMeasure);

            (SaveNumberMeasures as Command)?.ChangeCanExecute();
        }
        private bool CanSaveNumberMeasures(object obj)
        {
            return _oldNumberOfPulseMeasure != _numberOfPulseMeasure || _oldNumberOfOxigenMeasure != _numberOfOxigenMeasure;
        }
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

            ResetNumberMeasures = new Command(ExecuteResetNumberMeasures, CanResetNumberMeasures);
            SaveNumberMeasures = new Command(ExecuteSaveNumberMeasures, CanSaveNumberMeasures);
        }
    }
}
