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

        public event Action<string> ExceptionGenerated;

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
                    ExceptionGenerated.Invoke("Device got disconnected please scan again!");
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
                ExceptionGenerated.Invoke("Scanning was cancelled!");
                return false;
            }
            catch (Exception e)
            {
                ExceptionGenerated.Invoke("Error scanning for devices!");
                return false;
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }


            if (_device is null)
            {
                ExceptionGenerated.Invoke("Device not found!");
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
                    ExceptionGenerated.Invoke("Error connecting to the device!");
                    return false;
                }
                catch (TaskCanceledException tce)
                {
                    ExceptionGenerated.Invoke("Connection process was cancelled!");
                    return false;
                }
            }
        }
    }
}
