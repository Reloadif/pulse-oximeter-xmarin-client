using PulseOximeterApp.Infrastructure.Behaviors;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    public class IconLabel : Label
    {
        public event EventHandler<EventArgs> TextChanged;

        #region BindableProperty
        public static readonly BindableProperty TextIconProperty =
            BindableProperty.Create(nameof(TextIcon), typeof(string), typeof(IconLabel), "", propertyChanged: TextChangedHandler);

        public static readonly BindableProperty HasAnimationProperty = BindableProperty.Create(nameof(HasAnimation), typeof(bool), typeof(IntegerValidationBehavior), false);

        public string TextIcon
        {
            get => (string)GetValue(TextIconProperty);
            set => SetValue(TextIconProperty, value);
        }

        public bool HasAnimation
        {
            get => (bool)GetValue(HasAnimationProperty);
            set => SetValue(HasAnimationProperty, value);
        }
        #endregion

        private async static void TextChangedHandler(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as IconLabel;

            if (control.HasAnimation && (oldValue as string) != "")
            {
                await Task.WhenAll(control.FadeTo(0, 125), control.ScaleTo(0)).ContinueWith(t => Task.Delay(100));
            }

            control.TextChanged?.Invoke(control, new EventArgs());

            if (control.HasAnimation && (oldValue as string) != "")
            {
                await Task.WhenAll(control.FadeTo(1, 125), control.ScaleTo(1));
            }
        }
    }
}
