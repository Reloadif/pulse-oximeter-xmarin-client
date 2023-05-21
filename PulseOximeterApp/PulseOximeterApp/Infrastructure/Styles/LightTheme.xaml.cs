using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.Styles
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LightTheme : ResourceDictionary
    {
		public LightTheme ()
		{
			InitializeComponent ();
		}
	}
}