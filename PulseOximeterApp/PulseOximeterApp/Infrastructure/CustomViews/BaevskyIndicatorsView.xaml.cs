using PulseOximeterApp.Infrastructure.Effects;
using PulseOximeterApp.Models.HeartRate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaevskyIndicatorsView : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty BaevskyProperty = BindableProperty.Create(nameof(Baevsky), typeof(BaevskyIndicators), typeof(BaevskyIndicatorsView));

        public BaevskyIndicators Baevsky
        {
            get => (BaevskyIndicators)GetValue(BaevskyProperty);
            set => SetValue(BaevskyProperty, value);
        }
        #endregion

        public BaevskyIndicatorsView()
        {
            InitializeComponent();

            XamlABI.SetBinding(Label.TextProperty, new Binding("Baevsky.ABI", source: this));
            XamlVRI.SetBinding(Label.TextProperty, new Binding("Baevsky.VRI", source: this));
            XamlIARP.SetBinding(Label.TextProperty, new Binding("Baevsky.IARP", source: this));
            XamlVI.SetBinding(Label.TextProperty, new Binding("Baevsky.VI", source: this));
        }
    }
}