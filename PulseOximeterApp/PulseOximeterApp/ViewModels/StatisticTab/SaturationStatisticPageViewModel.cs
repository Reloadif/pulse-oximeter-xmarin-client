using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.StatisticTab;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    class SaturationStatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<SaturationStatistic> _saturationCollection;
        #endregion

        #region Properties
        public ObservableCollection<SaturationStatistic> SaturationCollection
        {
            get => _saturationCollection;
            set => Set(ref _saturationCollection, value);
        }

        public bool IsCollectionEmpty
        {
            get => SaturationCollection.Count == 0;
        }
        #endregion

        #region Commands
        public ICommand CollectionItemSelected { get; private set; }

        private async void ExecuteCollectionItemSelected(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new SingleSaturationPage { BindingContext = new SingleSaturationPageViewModel(obj as SaturationStatistic) });
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

        public SaturationStatisticPageViewModel()
        {
            SaturationCollection = new ObservableCollection<SaturationStatistic>(App.StatisticDataBase.GetSaturationStatisticsAsync().GetAwaiter().GetResult().OrderByDescending(s => s.ID));

            CollectionItemSelected = new Command(ExecuteCollectionItemSelected);
        }
    }
}
