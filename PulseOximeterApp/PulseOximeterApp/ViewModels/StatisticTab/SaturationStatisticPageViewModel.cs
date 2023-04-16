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
    class SaturationStatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<SaturationStatisticGroup> _saturationCollection;
        #endregion

        #region Properties
        public ObservableCollection<SaturationStatisticGroup> SaturationCollection
        {
            get => _saturationCollection;
            set => Set(ref _saturationCollection, value);
        }
        #endregion

        #region Commands
        public ICommand CollectionItemSelected { get; private set; }
        public ICommand DeleteCollectionItem { get; private set; }

        private async void ExecuteCollectionItemSelected(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new SingleSaturationPage { BindingContext = new SingleSaturationPageViewModel(obj as SaturationStatistic) });
        }

        private async void ExecuteDeleteCollectionItem(object obj)
        {
            if (obj is SaturationStatistic)
            {
                var saturationStatistic = obj as SaturationStatistic;
                await App.StatisticDataBase.DeleteSaturationStatisticAsync(saturationStatistic);
                RemoveSaturationStatisticFromCollection(saturationStatistic);
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

        public SaturationStatisticPageViewModel()
        {
            SaturationCollection = new ObservableCollection<SaturationStatisticGroup>(App.StatisticDataBase.GetSaturationStatisticsAsync().GetAwaiter().GetResult().
                OrderByDescending(s => s.ID).
                GroupBy(s => DateTime.Parse(s.AddedDate).ToString("D"),
                (key, group) => new SaturationStatisticGroup(key, new ObservableCollection<SaturationStatistic>(group.ToList())))
                );

            CollectionItemSelected = new Command(ExecuteCollectionItemSelected);
            DeleteCollectionItem = new Command(ExecuteDeleteCollectionItem);
        }

        private void RemoveSaturationStatisticFromCollection(SaturationStatistic saturationStatistic)
        {
            var pulseGroup = _saturationCollection.Where(pg => pg.Title == DateTime.Parse(saturationStatistic.AddedDate).ToString("D")).FirstOrDefault();

            pulseGroup.Remove(saturationStatistic);
            if (pulseGroup.Count == 0)
            {
                _saturationCollection.Remove(pulseGroup);

                if (_saturationCollection.Count == 0)
                {
                    SaturationCollection = null;
                }
            }
        }
    }
}
