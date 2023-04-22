using PulseOximeterApp.Data.DataBase;
using System;

namespace PulseOximeterApp.Models.HeartRate
{
    public class BaevskyIndicators
    {
        public double ABI { get; private set; }
        public double VRI { get; private set; }
        public double IARP { get; private set; }
        public double VI { get; private set; }

        public BaevskyIndicators(BaevskyIndicatorsRecord baevskyIndicatorsRecord)
        {
            ABI = baevskyIndicatorsRecord.ABI;
            VRI = baevskyIndicatorsRecord.VRI;
            IARP = baevskyIndicatorsRecord.IARP;
            VI = baevskyIndicatorsRecord.VI;
        }

        public BaevskyIndicators(HeartRateVariability heartRateVariability)
        {
            ABI = Math.Round(heartRateVariability.AMo / heartRateVariability.SDNN, 2);
            VRI = Math.Round(1 / (heartRateVariability.Mo * heartRateVariability.SDNN), 2);
            IARP = Math.Round(heartRateVariability.AMo / heartRateVariability.Mo, 2);
            VI = Math.Round(heartRateVariability.AMo / (2 * heartRateVariability.SDNN * heartRateVariability.Mo), 2);
        }
    }
}
