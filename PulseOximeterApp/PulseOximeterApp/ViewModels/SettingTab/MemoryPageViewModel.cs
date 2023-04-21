using PulseOximeterApp.Infrastructure.DependencyServices;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.ViewModels.HomeTab;
using PulseOximeterApp.Views.HomeTab;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.SettingTab
{
    class MemoryPageViewModel : BaseViewModel
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Commands
        public ICommand ClearPulseStatistics { get; private set; }
        public ICommand ClearSaturationStatistics { get; private set; }

        private async void ExecuteDeletePulseStatistics(object obj)
        {
            if (await DependencyService.Get<IShowMessageDependencyService>().ShowQuestionAsync("Подтвердить действие", "Удалить все данные о статистике измерений пульса?"))
            {
                await App.StatisticDataBase.ClearPulseStatisticTable();
                (ClearPulseStatistics as Command).ChangeCanExecute();
            }
        }
        private bool CanExecuteDeletePulseStatistics(object obj)
        {
            return App.StatisticDataBase.PulseRecordCount != 0;
        }

        private async void ExecuteClearSaturationStatistics(object obj)
        {
            if (await DependencyService.Get<IShowMessageDependencyService>().ShowQuestionAsync("Подтвердить действие", "Удалить все данные о статистике измерений сатурации?"))
            {
                await App.StatisticDataBase.ClearSaturationStatisticTable();
                (ClearSaturationStatistics as Command).ChangeCanExecute();
            }
        }

        private bool CanExecuteClearSaturationStatistics(object obj)
        {
            return App.StatisticDataBase.SaturationRecordCount != 0;
        }
        #endregion

        #region Base Methods
        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public async override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        #endregion

        public MemoryPageViewModel()
        {
            ClearPulseStatistics = new Command(ExecuteDeletePulseStatistics, CanExecuteDeletePulseStatistics);
            ClearSaturationStatistics = new Command(ExecuteClearSaturationStatistics, CanExecuteClearSaturationStatistics);
        }
    }
}
