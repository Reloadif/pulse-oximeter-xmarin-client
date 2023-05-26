using PulseOximeterApp.Infrastructure.DependencyServices;
using PulseOximeterApp.ViewModels.Base;
using System.Windows.Input;
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
        private async void ExecuteClearPulseStatistics(object obj)
        {
            if (await DependencyService.Get<IShowMessageDependencyService>().ShowQuestionAsync("Подтвердить действие", "Удалить все данные о статистике измерений пульса?"))
            {
                await App.StatisticDataBase.ClearPulseStatisticTable();
                (ClearPulseStatistics as Command).ChangeCanExecute();
            }
        }
        private bool CanExecuteClearPulseStatistics(object obj)
        {
            return App.StatisticDataBase.PulseRecordCount != 0;
        }

        public ICommand ClearSaturationStatistics { get; private set; }
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

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        #endregion

        public MemoryPageViewModel()
        {
            ClearPulseStatistics = new Command(ExecuteClearPulseStatistics, CanExecuteClearPulseStatistics);
            ClearSaturationStatistics = new Command(ExecuteClearSaturationStatistics, CanExecuteClearSaturationStatistics);
        }
    }
}
