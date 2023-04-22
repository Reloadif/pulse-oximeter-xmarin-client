using PulseOximeterApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PulseCommonInformationView : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty CommonInformationProperty = BindableProperty.Create(nameof(CommonInformation), typeof(PulseCommonInformation), typeof(PulseCommonInformationView));

        public PulseCommonInformation CommonInformation
        {
            get => (PulseCommonInformation)GetValue(CommonInformationProperty);
            set => SetValue(CommonInformationProperty, value);
        }
        #endregion

        public PulseCommonInformationView()
        {
            InitializeComponent();

            XamlAverageBPM.SetBinding(Label.TextProperty, new Binding("CommonInformation.AverageBPM", source: this));
            XamlNormalPulseMeasurement.SetBinding(Label.TextProperty, new Binding("CommonInformation.NormalPulseMeasurement", source: this));
            XamlRecommendation.SetBinding(Label.TextProperty, new Binding("CommonInformation.Recommendation", source: this));
        }
    }
}