using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Threading.Tasks;

namespace PulseOximeterApp.Services.BluetoothLE.Services
{
    class SaturationService : ISaturationService
    {
        private readonly IDevice _connectedDevice;
        private IService _pulseOximeterService;

        private ICharacteristic _measurementSelection;
        private ICharacteristic _saturationCharacteristic;

        private event Action<int> _saturationNotify;

        public bool IsDeviceConnected
        {
            get => _connectedDevice.State == Plugin.BLE.Abstractions.DeviceState.Connected;
        }

        public event Action<int> SaturationNotify
        {
            add
            {
                _saturationCharacteristic.ValueUpdated += OnSaturationValueUpdate;
                _saturationNotify += value;
            }
            remove
            {
                _saturationCharacteristic.ValueUpdated -= OnSaturationValueUpdate;
                _saturationNotify -= value;
            }
        }
        public async void StartMeasureSaturation()
        {
            if (!IsDeviceConnected) return;

            await _measurementSelection.WriteAsync(BitConverter.GetBytes(2));
            await _saturationCharacteristic.StartUpdatesAsync();
        }
        public async void StopMeasureSaturation()
        {
            if (!IsDeviceConnected) return;

            await _measurementSelection.WriteAsync(BitConverter.GetBytes(0));
            await _saturationCharacteristic.StopUpdatesAsync();
        }

        public SaturationService(IDevice connectedDevice)
        {
            _connectedDevice = connectedDevice;

            Task.Run(async () =>
            {
                _pulseOximeterService = await _connectedDevice.GetServiceAsync(Guid.Parse(Config.Config.PulseOximeterService));

                _saturationCharacteristic = await _pulseOximeterService.GetCharacteristicAsync(Guid.Parse(Config.Config.OxygenSatuartionCharacteristic));
                _measurementSelection = await _pulseOximeterService.GetCharacteristicAsync(Guid.Parse(Config.Config.MeasurementSelectionCharacteristic));
            }).GetAwaiter().GetResult();
        }
        private void OnSaturationValueUpdate(object sender, CharacteristicUpdatedEventArgs args)
        {
            _saturationNotify.Invoke(BitConverter.ToInt32(args.Characteristic.Value, 0));
        }
    }
}
