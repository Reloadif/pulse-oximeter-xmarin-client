using System;

namespace PulseOximeterApp.Services.DependencyServices
{
    public interface IGpsDependencyService
    {
        event Action<bool> GpsStatusChanged;

        bool IsGpsTurnedOn();
        void OpenSettings();
    }
}
