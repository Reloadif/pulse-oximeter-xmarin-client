using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Threading.Tasks;

namespace PulseOximeterApp.Services.BluetoothLE
{
    class PulseOximeterService : IPulseService, ISaturationService
    {
        private readonly IDevice _connectedDevice;
        private IService _pulseOximeterService;

        private ICharacteristic _measurementSelection;
        private ICharacteristic _lastBeatCharacteristic;
        private ICharacteristic _saturationCharacteristic;

        private event Action<int> _pulseNotify;
        private event Action<int> _saturationNotify;

        public bool IsDeviceConnected
        {
            get => _connectedDevice.State == Plugin.BLE.Abstractions.DeviceState.Connected;
        }

        public event Action<int> PulseNotify
        {
            add
            {
                _lastBeatCharacteristic.ValueUpdated += OnHeartBeatValueUpdate;
                _pulseNotify += value;
            }
            remove
            {
                _lastBeatCharacteristic.ValueUpdated -= OnHeartBeatValueUpdate;
                _pulseNotify -= value;
            }
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

        public async void StartMeasurePulse()
        {
            if (!IsDeviceConnected) return;

            await _measurementSelection.WriteAsync(BitConverter.GetBytes(1));
            await _lastBeatCharacteristic.StartUpdatesAsync();
        }
        public async void StopMeasurePulse()
        {
            if (!IsDeviceConnected) return;

            await _measurementSelection.WriteAsync(BitConverter.GetBytes(0));
            await _lastBeatCharacteristic.StopUpdatesAsync();
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

        public PulseOximeterService(IDevice connectedDevice)
        {
            _connectedDevice = connectedDevice;

            Task.Run(async () =>
            {
                _pulseOximeterService = await _connectedDevice.GetServiceAsync(Guid.Parse(Config.Config.PulseOximeterService));

                _lastBeatCharacteristic = await _pulseOximeterService.GetCharacteristicAsync(Guid.Parse(Config.Config.LastBeatCharacteristic));
                _saturationCharacteristic = await _pulseOximeterService.GetCharacteristicAsync(Guid.Parse(Config.Config.OxygenSatuartionCharacteristic));
                _measurementSelection = await _pulseOximeterService.GetCharacteristicAsync(Guid.Parse(Config.Config.MeasurementSelectionCharacteristic));
            }).GetAwaiter().GetResult();
        }

        private void OnHeartBeatValueUpdate(object sender, CharacteristicUpdatedEventArgs args)
        {
            _pulseNotify.Invoke(BitConverter.ToInt32(args.Characteristic.Value, 0));
        }
        private void OnSaturationValueUpdate(object sender, CharacteristicUpdatedEventArgs args)
        {
            _saturationNotify.Invoke(BitConverter.ToInt32(args.Characteristic.Value, 0));
        }
    }
}
