using System;
using System.Threading.Tasks;

namespace PulseOximeterApp.Infrastructure.DependencyServices
{
    public interface IMessageService
    {
        Task ShowAlertAsync(string message);
        Task<bool> ShowQuestionAsync(string title, string message);
    }

    public interface IGpsDependencyService
    {
        event Action<bool> GpsStatusChanged;

        bool IsGpsTurnedOn();
        void OpenSettings();
    }
}
