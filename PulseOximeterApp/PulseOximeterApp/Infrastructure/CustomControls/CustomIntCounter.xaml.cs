using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomIntCounter : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(CustomIntCounter), 0);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        public CustomIntCounter()
        {
            InitializeComponent();

            XamlCounterLabel.SetBinding(Label.TextProperty, new Binding("Value", source: this));
        }
    }
}