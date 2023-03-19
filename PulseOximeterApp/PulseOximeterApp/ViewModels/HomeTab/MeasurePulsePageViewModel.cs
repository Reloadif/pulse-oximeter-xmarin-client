using Plugin.BLE.Abstractions.Contracts;
using PulseOximeterApp.ViewModels.Base;
using System;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasurePulsePageViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDevice _device;
        #endregion

        #region Properties
        #endregion

        #region Events
        public event Action OnClosing;
        #endregion

        public MeasurePulsePageViewModel(IDevice device)
        {
            _device = device;
        }
    }
}
