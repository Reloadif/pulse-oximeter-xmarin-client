using ProgressRingControl.Forms.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressRingWithCounter : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(ProgressRingWithCounter), 0, propertyChanged: OnValuePropertyChanged);
        public static readonly BindableProperty NormalizedValueProperty = BindableProperty.Create(nameof(NormalizedValue), typeof(double), typeof(ProgressRingWithCounter), .0);
        public static readonly BindableProperty MaximumValueProperty = BindableProperty.Create(nameof(MaximumValue), typeof(int), typeof(ProgressRingWithCounter), int.MaxValue);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!oldValue.Equals(0))
            {
                (bindable as ProgressRingWithCounter).HandleValuePropertyChanged();
            }
        }

        public double NormalizedValue
        {
            get => (double)GetValue(NormalizedValueProperty);
            set => SetValue(NormalizedValueProperty, value);
        }
        public int MaximumValue
        {
            get => (int)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }
        #endregion

        public ProgressRingWithCounter()
        {
            InitializeComponent();

            XamlProgressRing.SetBinding(ProgressRing.ProgressProperty, new Binding("NormalizedValue", source: this));
            XamlCounterLabel.SetBinding(Label.TextProperty, new Binding("Value", source: this));
        }

        private void HandleValuePropertyChanged()
        {
            NormalizedValue = (MaximumValue - Value) / (double)MaximumValue;
        }
    }
}