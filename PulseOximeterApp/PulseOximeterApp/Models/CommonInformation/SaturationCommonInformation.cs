using PulseOximeterApp.Data.DataBase;
using System;

namespace PulseOximeterApp.Models.CommonInformation
{
    public class SaturationCommonInformation
    {
        public StatisticStatus Status { get; private set; }
        public int NormalPulseMeasurement { get; private set; }
        public string Recommendation { get; private set; }

        public SaturationCommonInformation(SaturationCommonInformationRecord saturationCommonInformationRecord)
        {
            Status = saturationCommonInformationRecord.Status;
            NormalPulseMeasurement = saturationCommonInformationRecord.NormalPulseMeasurement;
            Recommendation = saturationCommonInformationRecord.Recommendation;
        }

        public SaturationCommonInformation(int veryBad, int bad, int good, int measurementPoits)
        {
            int normalPulseMeasurement = Convert.ToInt32(Convert.ToDouble(good) / measurementPoits * 100);

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
