using PulseOximeterApp.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PulseOximeterApp.Models
{
    public class PulseCommonInformation
    {
        public StatisticStatus Status { get; private set; }
        public int AverageBPM { get; private set; }
        public int NormalPulseMeasurement { get; private set; }
        public string Recommendation { get; private set; }

        public PulseCommonInformation(PulseCommonInformationRecord pulseCommonInformationRecord)
        {
            Status = pulseCommonInformationRecord.Status;
            AverageBPM = pulseCommonInformationRecord.AverageBPM;
            NormalPulseMeasurement = pulseCommonInformationRecord.NormalPulseMeasurement;
            Recommendation = pulseCommonInformationRecord.Recommendation;
        }

        public PulseCommonInformation(IList<int> values)
        {
            int normalPercent = Convert.ToInt32(values.Where(v => v >= 45 && v <= 80).Count() / values.Count * 100);

            AverageBPM = Convert.ToInt32(values.Average());
            NormalPulseMeasurement = normalPercent;
            Status = StatisticStatus.Good;
            Recommendation = "";
        }
    }
}
