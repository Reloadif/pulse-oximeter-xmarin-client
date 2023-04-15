using PulseOximeterApp.Data.DataBase;
using System.Collections.ObjectModel;

namespace PulseOximeterApp.Models.GroupCollection
{
    internal class SaturationStatisticGroup : ObservableCollection<SaturationStatistic>
    {
        public string Title { get; private set; }

        public SaturationStatisticGroup(string title, ObservableCollection<SaturationStatistic> collection) : base(collection)
        {
            Title = title;
        }
    }
}
