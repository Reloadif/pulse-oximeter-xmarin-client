using System;
using System.Threading;

namespace PulseOximeterApp.Services.BluetoothLE
{
    internal interface IPulseService
    {
        event Action<int> PulseNotify;

        void StartMeasurePulse(CancellationToken token);
    }

    internal interface ISaturationService
    {
        event Action<int> SaturationNotify;

        void StartMeasureSaturation(CancellationToken token);
    }
}
