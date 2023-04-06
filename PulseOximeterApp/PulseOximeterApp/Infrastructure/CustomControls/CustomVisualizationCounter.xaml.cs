using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomVisualizationCounter : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(CustomVisualizationCounter), 0, propertyChanged: OnValuePropertyChanged);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!oldValue.Equals(0))
            {
                (bindable as CustomVisualizationCounter).HandleValuePropertyChanged();
            }
        }
        #endregion

        private double XamlStrokeEllipseScale;

        public CustomVisualizationCounter()
        {
            InitializeComponent();

            XamlCounterLabel.SetBinding(Label.TextProperty, new Binding("Value", source: this));
            XamlStrokeEllipseScale = XamlStrokeEllipse.Scale;
        }

        private async void HandleValuePropertyChanged()
        {
            await XamlStrokeEllipse.ScaleTo(XamlStrokeEllipseScale * 1.15);
            await XamlStrokeEllipse.ScaleTo(XamlStrokeEllipseScale);
        }
    }
}