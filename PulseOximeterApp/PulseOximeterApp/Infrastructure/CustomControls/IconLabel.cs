using System;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    public class IconLabel : Label
    {
        public event EventHandler<EventArgs> TextChanged;

        #region BindableProperty
        public static readonly new BindableProperty TextProperty =
            BindableProperty.Create(
                propertyName: nameof(Text),
                returnType: typeof(string),
                declaringType: typeof(IconLabel),
                defaultValue: "",
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: TextChangedHandler);

        public new string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                base.Text = value;
                SetValue(TextProperty, value);
            }
        }
        #endregion

        private static void TextChangedHandler(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as IconLabel;
            control.TextChanged?.Invoke(control, new EventArgs());
        }
    }
}
