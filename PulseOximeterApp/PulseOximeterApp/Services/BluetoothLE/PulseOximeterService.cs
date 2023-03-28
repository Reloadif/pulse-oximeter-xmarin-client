using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace PulseOximeterApp.Services.BluetoothLE
{
    class PulseOximeterService : IPulseService, ISaturationService
    {
        private readonly IDevice _connectedDevice;
        private IService _pulseOximeterService;

        private ICharacteristic _measurementSelection;
        private ICharacteristic _heartBeatCharacteristic;
        private ICharacteristic _saturationCharacteristic;

        private event Action<int> _pulseNotify;
        private event Action<int> _saturationNotify;

        public event Action<int> PulseNotify
        {
            add
            {
                _heartBeatCharacteristic.ValueUpdated += OnHeartBeatValueUpdate;
                _pulseNotify += value;
            }
            remove
            {
                _heartBeatCharacteristic.ValueUpdated -= OnHeartBeatValueUpdate;
                _pulseNotify -= value;

                Task.Run(async () =>
                {
                    await _measurementSelection.WriteAsync(BitConverter.GetBytes(0));
                }).GetAwaiter().GetResult();
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

                Task.Run(async () =>
                {
                    await _measurementSelection.WriteAsync(BitConverter.GetBytes(0));
                }).GetAwaiter().GetResult();
            }
        }

        public async void StartMeasurePulse(CancellationToken token)
        {
            await _measurementSelection.WriteAsync(BitConverter.GetBytes(1));
            await _heartBeatCharacteristic.StartUpdatesAsync(token);
        }
        public async void StartMeasureSaturation(CancellationToken token)
        {
            await _measurementSelection.WriteAsync(BitConverter.GetBytes(2));
            await _saturationCharacteristic.StartUpdatesAsync(token);
        }

        public PulseOximeterService(IDevice connectedDevice)
        {
            _connectedDevice = connectedDevice;

            Task.Run(async () =>
            {
                _pulseOximeterService = await _connectedDevice.GetServiceAsync(Guid.Parse(Config.Config.PulseOximeterService));

                _heartBeatCharacteristic = await _pulseOximeterService.GetCharacteristicAsync(Guid.Parse(Config.Config.HeartBeatCharacteristic));
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
