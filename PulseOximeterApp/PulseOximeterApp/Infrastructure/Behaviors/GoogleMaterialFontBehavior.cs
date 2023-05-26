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
            {"done", '\ue876'},
            {"close", '\ue5cd'},
            {"monitor_heart", '\ueaa2'},
            {"looks_two", '\ue401' },
            {"delete_forever", '\ue92b' },
            {"restart_alt", '\uf053' },
            {"save", '\ue161' },
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

            if (label?.TextIcon?.Length >= 2 && iconCodeDict.TryGetValue(label.TextIcon, out var icon))
            {
                label.FontFamily = fontFamily;
                label.Text = icon.ToString();
            }
        }
    }
}
