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

        private readonly string _deviceName;
        private CancellationTokenSource _cancelTokenSource;

        public IDevice GetDevice
        {
            get => _device;
        }

        public MicrocontrollerConnector()
        {
            _adapter = CrossBluetoothLE.Current.Adapter;
            _device = null;

            _deviceName = "ESP32-C3-PULSE-OXIMETER";

            _adapter.DeviceDiscovered += (sender, args) =>
            {
                if (args.Device.Name != null && args.Device.Name.Equals(_deviceName))
                {
                    _device = args.Device;
                    _cancelTokenSource.Cancel();
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
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alert", "Scanning was cancelled", "OK");
                return false;
            }
            catch (Exception e)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alert", "Error scanning for devices", "OK");
                return false;
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }


            if (_device is null)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alert", "Device not found!", "OK");
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
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alert", "Error connecting to the device!", "OK");
                    return false;
                }
                catch (TaskCanceledException tce)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alert", "Connection process was cancelled!", "OK");
                    return false;
                }
            }
        }
    }
}
