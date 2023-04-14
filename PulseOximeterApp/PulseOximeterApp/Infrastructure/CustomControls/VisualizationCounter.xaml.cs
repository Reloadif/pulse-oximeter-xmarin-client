using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizationCounter : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(VisualizationCounter), 0, propertyChanged: OnValuePropertyChanged);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!oldValue.Equals(0))
            {
                (bindable as VisualizationCounter).HandleValuePropertyChanged();
            }
        }
        #endregion

        private double _xamlStrokeEllipseScale;

        public VisualizationCounter()
        {
            InitializeComponent();

            XamlCounterLabel.SetBinding(Label.TextProperty, new Binding("Value", source: this));
            _xamlStrokeEllipseScale = XamlStrokeEllipse.Scale;
        }

        private async void HandleValuePropertyChanged()
        {
            await XamlStrokeEllipse.ScaleTo(_xamlStrokeEllipseScale * 1.15);
            await XamlStrokeEllipse.ScaleTo(_xamlStrokeEllipseScale);
        }
    }
}