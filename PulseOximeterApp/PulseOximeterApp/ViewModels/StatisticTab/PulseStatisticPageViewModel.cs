using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Models.GroupCollection;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.StatisticTab;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    class PulseStatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<PulseStatisticGroup> _pulseCollection;
        #endregion

        #region Properties
        public ObservableCollection<PulseStatisticGroup> PulseCollection
        {
            get => _pulseCollection;
            set => Set(ref _pulseCollection, value);
        }
        #endregion

        #region Commands
        public ICommand CollectionItemSelected { get; private set; }
        public ICommand DeleteCollectionItem { get; private set; }

        private async void ExecuteCollectionItemSelected(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new SinglePulsePage { BindingContext = new SinglePulsePageViewModel(obj as PulseStatistic) });
        }

        private async void ExecuteDeleteCollectionItem(object obj)
        {
            if (obj is PulseStatistic)
            {
                var pulseStatistic = obj as PulseStatistic;
                await App.StatisticDataBase.DeletePulseStatisticAsync(pulseStatistic);
                RemovePulseStatisticFromCollection(pulseStatistic);
            }
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
            PulseCollection = new ObservableCollection<PulseStatisticGroup>(App.StatisticDataBase.GetPulseStatisticsAsync().GetAwaiter().GetResult().
                OrderByDescending(s => s.ID).
                GroupBy(s => DateTime.Parse(s.AddedDate).ToString("D"),
                (key, group) => new PulseStatisticGroup(key, new ObservableCollection<PulseStatistic>(group.ToList())))
                );

            CollectionItemSelected = new Command(ExecuteCollectionItemSelected);
            DeleteCollectionItem = new Command(ExecuteDeleteCollectionItem);
        }

        private void RemovePulseStatisticFromCollection(PulseStatistic pulseStatistic)
        {
            var pulseGroup = _pulseCollection.Where(pg => pg.Title == DateTime.Parse(pulseStatistic.AddedDate).ToString("D")).FirstOrDefault();
            pulseGroup.Remove(pulseStatistic);
        }
    }
}
