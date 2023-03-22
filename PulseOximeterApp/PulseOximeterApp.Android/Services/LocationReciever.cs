using Android.Content;
using Android.Locations;
using System;

namespace PulseOximeterApp.Droid.Services
{
    public class LocationReciever : BroadcastReceiver
    {
        private bool _oldValue;
        private bool _newValue;

        public event Action<bool> GpsStatusChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            if (LocationManager.ProvidersChangedAction.Equals(intent.Action))
            {
                LocationManager locationManager = (LocationManager)context.GetSystemService(Context.LocationService);
                _oldValue = _newValue;
                _newValue = locationManager.IsProviderEnabled(LocationManager.GpsProvider);

                if (_oldValue != _newValue)
                {
                    GpsStatusChanged.Invoke(true);
                }
            }
        }
    };
}