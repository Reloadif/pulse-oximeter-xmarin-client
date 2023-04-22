using SkiaSharp;

namespace PulseOximeterApp.Services
{
    public class ChartEntryColor
    {
        public static readonly SKColor PulseDefault = SKColor.Parse("f24518");
        public static readonly SKColor PulseVeryLow = SKColor.Parse("f24518");
        public static readonly SKColor PulseLow = SKColor.Parse("f1f518");
        public static readonly SKColor PulseNormal = SKColor.Parse("2bf518");

        public static readonly SKColor SaturationDefault = SKColor.Parse("f24518");
        public static readonly SKColor SaturationVeryLow = SKColor.Parse("f24518");
        public static readonly SKColor SaturationLow = SKColor.Parse("f1f518");
        public static readonly SKColor SaturationNormal = SKColor.Parse("2bf518");
    }

    public class ChartEntryToSKColorConverter
    {
        public static SKColor FromPulse(int value)
        {
            SKColor result;
            if (value < 45) result = ChartEntryColor.PulseVeryLow;
            else if (value <= 80) result = ChartEntryColor.PulseNormal;
            else if (value <= 120) result = ChartEntryColor.PulseLow;
            else result = ChartEntryColor.PulseVeryLow;

            return result;
        }

        public static SKColor FromSaturation(int value)
        {
            SKColor result = ChartEntryColor.SaturationDefault;

            if (value < 90) result = ChartEntryColor.SaturationVeryLow;
            else if (value < 95) result = ChartEntryColor.SaturationLow;
            else if (value <= 100) result = ChartEntryColor.SaturationNormal;

            return result;
        }
    }
}
