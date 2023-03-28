
namespace PulseOximeterApp.Services.BluetoothLE.Config
{
    internal class Config
    {
        public static string DeviceName = "ESP32-C3-PULSE-OXIMETER";
        public static string PulseOximeterService = "5fafc201-1fb5-459e-8fcc-000000000000";

        public static string MeasurementSelectionCharacteristic = "5fafc201-1fb5-459e-8fcc-000000000001";
        public static string HeartBeatCharacteristic = "5fafc201-1fb5-459e-8fcc-000000000002";
        public static string OxygenSatuartionCharacteristic = "5fafc201-1fb5-459e-8fcc-000000000003";
    }
}