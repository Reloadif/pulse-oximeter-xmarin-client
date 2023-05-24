using System;

namespace PulseOximeterApp.Services.BluetoothLE
{
    public interface IPulseService
    {
        event Action<int> PulseNotify;

        void StartMeasurePulse();
        void StopMeasurePulse();
    }

    public interface ISaturationService
    {
        event Action<int> SaturationNotify;

        void StartMeasureSaturation();
        void StopMeasureSaturation();
    }
}
