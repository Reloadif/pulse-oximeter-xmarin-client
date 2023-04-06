using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            _adapter.DeviceDiscovered += OnDeviceDiscovered;
            _adapter.DeviceConnectionLost += OnDeviceConnectionLost;
        }

        ~MicrocontrollerConnector()
        {
            _adapter.DeviceDiscovered -= OnDeviceDiscovered;
            _adapter.DeviceConnectionLost -= OnDeviceConnectionLost;
        }

        public async Task<bool> Connect()
        {
            if (_device != null && _device.State != Plugin.BLE.Abstractions.DeviceState.Disconnected) return true;

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

        private void OnDeviceDiscovered(object sender, DeviceEventArgs args)
        {
            if (args.Device.Name != null && args.Device.Name.Equals(Config.Config.DeviceName))
            {
                _device = args.Device;
                _cancelTokenSource.Cancel();
            }
        }
        private void OnDeviceConnectionLost(object sender, DeviceEventArgs args)
        {
            if (args.Device.Name.Equals(Config.Config.DeviceName))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.DisplayAlert("Alert", "Connection to device lost, please connect again!", "OK");
                    await Shell.Current.Navigation.PopToRootAsync();
                });
            }
        }
    }
}
