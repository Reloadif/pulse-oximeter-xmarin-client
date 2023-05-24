using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using PulseOximeterApp.Droid.Permission;
using PulseOximeterApp.Droid.Services;
using Xamarin.Essentials;

namespace PulseOximeterApp.Droid
{
    [Activity(Label = "Pulse Oximeter", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly LocationReciever locationSwitchStateReceiver = new LocationReciever();
        private readonly IntentFilter intentFilter = new IntentFilter(LocationManager.ProvidersChangedAction);

        public static MainActivity Instance;

        public LocationReciever GpsReciever
        {
            get { return locationSwitchStateReceiver; }
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            intentFilter.AddAction(Intent.ActionProviderChanged);
            RegisterReceiver(locationSwitchStateReceiver, intentFilter);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;

            Instance = this;

            await Permissions.RequestAsync<BLEPermission>();
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