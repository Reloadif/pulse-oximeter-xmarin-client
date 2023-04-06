using System;
using System.Collections.Generic;
using System.Text;

namespace PulseOximeterApp.Models.HeartRate
{
    internal class BaevskyIndicators
    {
        private readonly HeartRateVariability _heartRateVariability;

        public double ABI { get; private set; }
        public double VRI { get; private set; }
        public double IARP { get; private set; }
        public double VI { get; private set; }

        public BaevskyIndicators(HeartRateVariability heartRateVariability)
        {
            _heartRateVariability = heartRateVariability;

            ABI = Math.Round(_heartRateVariability.AMo / _heartRateVariability.SDNN, 2);
            VRI = Math.Round(1 / (_heartRateVariability.Mo * _heartRateVariability.SDNN), 2);
            IARP = Math.Round(_heartRateVariability.AMo / _heartRateVariability.Mo, 2);
            VI = Math.Round(_heartRateVariability.AMo / (2 * _heartRateVariability.SDNN * _heartRateVariability.Mo), 2);
        }
    }
}
