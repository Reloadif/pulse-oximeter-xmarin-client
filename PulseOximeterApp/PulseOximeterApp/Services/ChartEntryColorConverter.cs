using SkiaSharp;

namespace PulseOximeterApp.Services
{
    public class ChartEntryColor
    {
        public static readonly SKColor SaturationVeryLow = SKColor.Parse("f24518");
        public static readonly SKColor SaturationLow = SKColor.Parse("f1f518");
        public static readonly SKColor SaturationNormal = SKColor.Parse("2bf518");
    }

    public class ChartEntryColorConverter
    {
        public static SKColor FromPulse(int value)
        {
            SKColor result = SKColor.Parse("f24518");

            if (value < 45) result = SKColor.Parse("f24518");
            else if (value < 80) result = SKColor.Parse("2bf518");
            else if (value < 100) result = SKColor.Parse("f1f518");

            return result;
        }

        public static SKColor FromSaturation(int value)
        {
            SKColor result = ChartEntryColor.SaturationVeryLow;

            if (value < 90) result = ChartEntryColor.SaturationVeryLow;
            else if (value < 95) result = ChartEntryColor.SaturationLow;
            else if (value <= 100) result = ChartEntryColor.SaturationNormal;

            return result;
        }
    }
}
