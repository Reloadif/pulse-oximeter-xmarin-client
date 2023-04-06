using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using PulseOximeterApp.Droid.Services;

namespace PulseOximeterApp.Droid
{
    [Activity(Label = "Pulse Oximeter", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly LocationReciever locationSwitchStateReceiver = new LocationReciever();
        private readonly IntentFilter intentFilter = new IntentFilter(LocationManager.ProvidersChangedAction);

        public static MainActivity Instance;

        public LocationReciever GpsReciever
        {
            get { return locationSwitchStateReceiver; }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            intentFilter.AddAction(Intent.ActionProviderChanged);
            RegisterReceiver(locationSwitchStateReceiver, intentFilter);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Instance = this;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnPause()
        {
            UnregisterReceiver(locationSwitchStateReceiver);

            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();

            RegisterReceiver(locationSwitchStateReceiver, intentFilter);
        }
    }
}