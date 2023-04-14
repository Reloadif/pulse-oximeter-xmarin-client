using PulseOximeterApp.Infrastructure.CustomControls;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.Behaviors
{
    public class GoogleMaterialFontBehavior : Behavior<IconLabel>
    {
        private static readonly string fontFamily = Device.RuntimePlatform == Device.Android ? "MaterialIcons-Regular.ttf#MaterialIcons-Regular" : "MaterialIcons-Regular";

        private static readonly Dictionary<string, char> iconCodeDict = new Dictionary<string, char>
        {
            {"monitor_heart", '\ueaa2'},
            {"looks_two", '\ue401' },
        };

        protected override void OnAttachedTo(IconLabel bindable)
        {
            HandleTextChanged(bindable, null);
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(IconLabel bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void HandleTextChanged(object sender, EventArgs e)
        {
            var label = (IconLabel)sender;

            if (label?.Text?.Length >= 2 && iconCodeDict.TryGetValue(label.Text, out var icon))
            {
                label.FontFamily = fontFamily;
                label.Text = icon.ToString();
            }
        }
    }
}
