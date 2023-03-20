using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PulseOximeterApp.Services.BluetoothLE
{
    internal class MicrocontrollerConnector
    {
        private readonly IAdapter _adapter;
        private IDevice _device;

        private CancellationTokenSource _cancelTokenSource;

        public IDevice GetDevice
        {
            get => _device;
        }

        public event Action<string> OnException;

        public MicrocontrollerConnector()
        {
            _adapter = CrossBluetoothLE.Current.Adapter;
            _device = null;

            _adapter.DeviceDiscovered += (sender, args) =>
            {
                if (args.Device.Name != null && args.Device.Name.Equals(Config.Config.DeviceName))
                {
                    _device = args.Device;
                    _cancelTokenSource.Cancel();
                }
            };

            _adapter.DeviceDisconnected += (sender, args) =>
            {
                if (args.Device.Name.Equals(Config.Config.DeviceName))
                {
                    OnException.Invoke("Device got disconnected please scan again!");
                }
            };
        }

        public async Task<bool> Connect()
        {
            _device = null;
            _cancelTokenSource = new CancellationTokenSource();

            try
            {
                await _adapter.StartScanningForDevicesAsync(cancellationToken: _cancelTokenSource.Token);
            }
            catch (TaskCanceledException tce)
            {
                OnException.Invoke("Scanning was cancelled!");
                return false;
            }
            catch (Exception e)
            {
                OnException.Invoke("Error scanning for devices!");
                return false;
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }


            if (_device is null)
            {
                OnException.Invoke("Device not found!");
                return false;
            }
            else
            {
                try
                {
                    await _adapter.ConnectToDeviceAsync(_device);
                    return true;
                }
                catch (DeviceConnectionException dce)
                {
                    OnException.Invoke("Error connecting to the device!");
                    return false;
                }
                catch (TaskCanceledException tce)
                {
                    OnException.Invoke("Connection process was cancelled!");
                    return false;
                }
            }
        }
    }
}
