using SkiaSharp;

namespace PulseOximeterApp.Services
{
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
            SKColor result = SKColor.Parse("f24518");

            if (value < 90) result = SKColor.Parse("f24518");
            else if (value < 95) result = SKColor.Parse("f1f518");
            else if (value <= 100) result = SKColor.Parse("2bf518");

            return result;
        }
    }
}
