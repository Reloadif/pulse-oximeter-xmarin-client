using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PulseOximeterApp.Services.BluetoothLE
{
    class PulseOximeterService : IPulseService, ISaturationService
    {
        private readonly IDevice _connectedDevice;

        private IService _heartBeatService;
        private ICharacteristic _heartBeatCharacteristic;

        public event Action<int> OnPulseNotify;
        public event Action<int> OnSaturationNotify;

        public async void StartMeasurePulse(CancellationToken token)
        {
            _heartBeatService = await _connectedDevice.GetServiceAsync(Guid.Parse(Config.Config.HeartBeatService));
            _heartBeatCharacteristic = await _heartBeatService.GetCharacteristicAsync(Guid.Parse(Config.Config.HeartBeatCharacteristic));

            _heartBeatCharacteristic.ValueUpdated += (sender, args) =>
            {
                OnPulseNotify.Invoke(BitConverter.ToInt32(args.Characteristic.Value, 0));
            };

            await _heartBeatCharacteristic.StartUpdatesAsync(token);
        }

        public async void StartMeasureSaturation(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public PulseOximeterService(IDevice connectedDevice)
        {
            _connectedDevice = connectedDevice;
        }
    }
}
