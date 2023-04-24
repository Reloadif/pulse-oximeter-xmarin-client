using PulseOximeterApp.Data.DataBase;
using System.Collections.ObjectModel;

namespace PulseOximeterApp.Models.GroupCollection
{
    public class PulseStatisticGroup : ObservableCollection<PulseStatistic>
    {
        public string Title { get; private set; }

        public PulseStatisticGroup(string title, ObservableCollection<PulseStatistic> collection) : base(collection)
        {
            Title = title;
        }
    }
}
