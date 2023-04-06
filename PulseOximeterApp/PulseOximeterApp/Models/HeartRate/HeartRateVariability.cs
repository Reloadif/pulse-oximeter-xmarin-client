using System;
using System.Collections.Generic;
using System.Linq;

namespace PulseOximeterApp.Models
{
    internal class HeartRateVariability
    {
        private readonly IList<double> _sampleCardioIntervals;

        public double DRR { get; private set; }
        public double RRNN { get; private set; }
        public double SDNN { get; private set; }
        public double CV { get; private set; }
        public double RMSSD { get; private set; }
        public double PNN50 { get; private set; }

        public double Mo { get; private set; }
        public double AMo { get; private set; }

        public HeartRateVariability(IList<double> sampleCardioIntervals)
        {
            _sampleCardioIntervals = sampleCardioIntervals;

            DRR = _sampleCardioIntervals.Max() - _sampleCardioIntervals.Min();
            RRNN = _sampleCardioIntervals.Average();

            SDNN = Math.Sqrt(_sampleCardioIntervals.Sum(v => Math.Pow(v - RRNN, 2)) / (_sampleCardioIntervals.Count - 1));
            CV = (SDNN / RRNN) * 100;

            RMSSD = Math.Sqrt(_sampleCardioIntervals.Aggregate((x, y) => Math.Pow(x - y, 2)) / (_sampleCardioIntervals.Count - 1));
            PNN50 = (_sampleCardioIntervals.Aggregate((x, y) => Math.Abs(y - x) > 0.05 ? 1 : 0) / _sampleCardioIntervals.Count) * 100;

            NBucket nBucket = new NBucket(_sampleCardioIntervals, 0.05);
            Mo = nBucket.GetModeValue();
            AMo = (nBucket.GetModeCount() / Convert.ToDouble(_sampleCardioIntervals.Count)) * 100;
        }
    }

    internal class NBucket
    {
        private readonly double _start;
        private readonly double _end;
        private readonly double _step;

        private readonly IList<int> _buckets;

        public NBucket(IList<double> values, double step)
        {
            _start = values.Min();
            _end = values.Max();
            _step = step;

            _buckets = new List<int>();

            for (int i = 0; i < Convert.ToInt32((_end - _start) / _step); i++)
            {
                _buckets.Add(0);
            }

            foreach (double value in values)
            {
                _buckets[CalculateKey(value)] += 1;
            }
        }

        public int GetModeCount()
        {
            return _buckets.Max();
        }
        public double GetModeValue()
        {
            int indexMax = _buckets.IndexOf(_buckets.Max());
            return _buckets[indexMax - 1] > _buckets[indexMax + 1] ? _start + _step * (indexMax + 0.25) : _start + _step * (indexMax + 0.75);
        }

        private int CalculateKey(double value)
        {
            return Math.Min(Convert.ToInt32((value - _start) / _step), _buckets.Count - 1);
        }
    }
}
