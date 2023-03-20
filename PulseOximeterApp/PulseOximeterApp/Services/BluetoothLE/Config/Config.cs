using System;
using System.Collections.Generic;
using System.Text;

namespace PulseOximeterApp.Services.BluetoothLE.Config
{
    internal class Config
    {
        public static string DeviceName = "ESP32-C3-PULSE-OXIMETER";
        public static string HeartBeatService = "5fafc201-1fb5-459e-8fcc-c5c9c331914b";
        public static string SaturationService = "5fafc201-1fb5-459e-8fcc-c5c9c331914b";

        public static string FingerDetectedCharacteristic = "beb5483e-36e1-4688-b7f5-ea07361b26a8";
        public static string HeartBeatCharacteristic = "24bfa272-7d7f-4b8f-baf3-08e60f038479";
    }
}
