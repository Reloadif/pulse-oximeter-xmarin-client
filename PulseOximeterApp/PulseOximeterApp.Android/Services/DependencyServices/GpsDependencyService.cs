using Android.Content;
using Android.Locations;
using PulseOximeterApp.Droid.Services.DependencyServices;
using PulseOximeterApp.Infrastructure.DependencyServices;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(GpsDependencyService))]
namespace PulseOximeterApp.Droid.Services.DependencyServices
{
    public class GpsDependencyService : IGpsDependencyService
    {
        public event Action<bool> GpsStatusChanged;

        public bool IsGpsTurnedOn()
        {
            LocationManager locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            return locationManager.IsProviderEnabled(LocationManager.GpsProvider);
        }

        public void OpenSettings()
        {
            Intent intent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            try
            {
                Android.App.Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException activityNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine(activityNotFoundException.Message);
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "Error: Gps Activity", Android.Widget.ToastLength.Short).Show();
            }
        }

        public GpsDependencyService()
        {
            MainActivity.Instance.GpsReciever.GpsStatusChanged += OnGpsStatusChanged;
        }

        ~GpsDependencyService()
        {
            MainActivity.Instance.GpsReciever.GpsStatusChanged -= OnGpsStatusChanged;
        }

        private void OnGpsStatusChanged(bool value)
        {
            GpsStatusChanged.Invoke(value);
        }
    }
}