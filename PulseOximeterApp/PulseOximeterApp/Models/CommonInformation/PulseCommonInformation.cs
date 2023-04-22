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
            int normalPulseMeasurement = Convert.ToInt32(values.Where(v => v >= 45 && v <= 80).Count() / values.Count * 100);

            AverageBPM = Convert.ToInt32(values.Average());
            NormalPulseMeasurement = normalPulseMeasurement;
            Status = CalculateStatisticStatus(normalPulseMeasurement);
            Recommendation = CalculateRecommendetion(normalPulseMeasurement);
        }

        private StatisticStatus CalculateStatisticStatus(int normalPulseMeasurement)
        {
            StatisticStatus result = StatisticStatus.Default;

            if (normalPulseMeasurement < 80) result = StatisticStatus.VeryBad;
            else if (normalPulseMeasurement < 90) result = StatisticStatus.Bad;
            else if (normalPulseMeasurement <= 100) result = StatisticStatus.Good;

            return result;
        }

        private string CalculateRecommendetion(int normalPulseMeasurement)
        {
            string result = "";

            if (normalPulseMeasurement < 80) result = "Состояние оценивается, как очень плохое!";
            else if (normalPulseMeasurement < 90) result = "Состояние оценивается, как плохое!";
            else if (normalPulseMeasurement <= 100) result = "Состояние оценивается, как хорошое!";

            return result;
        }
    }
}
