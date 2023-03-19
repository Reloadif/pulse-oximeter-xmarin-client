using PulseOximeterApp.ViewModels.Base;

namespace PulseOximeterApp.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        #region Fields
        private bool _homeTabIsEnabled = true;
        private bool _statisticTabIsEnabled = true;
        private bool _settingTabIsEnabled = true;
        #endregion

        #region Properties
        public bool HomeTabIsEnabled
        {
            get => _homeTabIsEnabled;
            set => Set(ref _homeTabIsEnabled, value);
        }
        public bool StatisticTabIsEnabled
        {
            get => _statisticTabIsEnabled;
            set => Set(ref _statisticTabIsEnabled, value);
        }
        public bool SettingTabIsEnabled
        {
            get => _settingTabIsEnabled;
            set => Set(ref _settingTabIsEnabled, value);
        }
        #endregion
    }
}
