using PulseOximeterApp.Models.CommonInformation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SaturationCommonInformationView : ContentView
	{
        #region BindableProperty
        public static readonly BindableProperty CommonInformationProperty = BindableProperty.Create(nameof(CommonInformation), typeof(SaturationCommonInformation), typeof(SaturationCommonInformationView));

        public SaturationCommonInformation CommonInformation
        {
            get => (SaturationCommonInformation)GetValue(CommonInformationProperty);
            set => SetValue(CommonInformationProperty, value);
        }
        #endregion

        public SaturationCommonInformationView ()
		{
			InitializeComponent ();

            XamlNormalSaturationMeasurement.SetBinding(Label.TextProperty, new Binding("CommonInformation.NormalPulseMeasurement", source: this));
            XamlRecommendation.SetBinding(Label.TextProperty, new Binding("CommonInformation.Recommendation", source: this));
        }
	}
}