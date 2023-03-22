using CoreLocation;
using Foundation;
using PulseOximeterApp.iOS.Services.DependencyServices;
using PulseOximeterApp.Services.DependencyServices;
using System;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GpsDependencyService))]
namespace PulseOximeterApp.iOS.Services.DependencyServices
{
    public class GpsDependencyService : IGpsDependencyService
    {
        public event Action<bool> GpsStatusChanged;

        public bool IsGpsTurnedOn()
        {
            return CLLocationManager.Status == CLAuthorizationStatus.Denied;
        }

        public void OpenSettings()
        {
            var WiFiURL = new NSUrl("prefs:root=WIFI");

            if (UIApplication.SharedApplication.CanOpenUrl(WiFiURL))
            {   //> Pre iOS 10
                UIApplication.SharedApplication.OpenUrl(WiFiURL);
            }
            else
            {   //> iOS 10
                UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));
            }
        }
    }
}