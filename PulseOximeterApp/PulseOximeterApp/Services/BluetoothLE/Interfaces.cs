using System;
using System.Threading;

namespace PulseOximeterApp.Services.BluetoothLE
{
    internal interface IPulseService
    {
        event Action<int> OnPulseNotify;

        void StartMeasurePulse(CancellationToken token);
    }

    internal interface ISaturationService
    {
        event Action<int> OnSaturationNotify;

        void StartMeasureSaturation(CancellationToken token);
    }
}
