using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.StatisticTab;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    class PulseStatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<PulseStatistic> _pulseCollection;
        #endregion

        #region Properties
        public ObservableCollection<PulseStatistic> PulseCollection
        {
            get => _pulseCollection;
            set => Set(ref _pulseCollection, value);
        }
        
        public bool IsCollectionEmpty
        {
            get => PulseCollection.Count == 0;
        }
        #endregion

        #region Commands
        public ICommand CollectionItemSelected { get; private set; }

        private async void ExecuteCollectionItemSelected(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new SinglePulsePage { BindingContext = new SinglePulsePageViewModel(obj as PulseStatistic) });
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

        public PulseStatisticPageViewModel()
        {
            PulseCollection = new ObservableCollection<PulseStatistic>(App.StatisticDataBase.GetPulseStatisticsAsync().GetAwaiter().GetResult().OrderByDescending(s => s.ID));

            CollectionItemSelected = new Command(ExecuteCollectionItemSelected);
        }
    }
}
