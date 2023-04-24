using System;
using PulseOximeterApp.Infrastructure.Effects;
using Com.Tomergoldst.Tooltips;
using ToolTipSample.Droid.Effects;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Com.Tomergoldst.Tooltips.ToolTipsManager;

[assembly: ResolutionGroupName("PulseOximeterApp")]
[assembly: ExportEffect(typeof(DroidTooltipEffect), nameof(TooltipEffect))]
namespace ToolTipSample.Droid.Effects
{
    public class DroidTooltipEffect : PlatformEffect
    {
        readonly ITipListener _listener;
        readonly ToolTipsManager _toolTipsManager;
        ToolTip _toolTipView;

        public DroidTooltipEffect()
        {
            _listener = new TipListener();
            _toolTipsManager = new ToolTipsManager(_listener);
        }

        protected override void OnAttached()
        {
            var control = Control ?? Container;
            control.Click += OnTap;
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Click -= OnTap;
            _toolTipsManager.FindAndDismiss(control);
        }

        void OnTap(object sender, EventArgs e)
        {
            var control = Control ?? Container;
            var text = TooltipEffect.GetText(Element);

            if (!string.IsNullOrEmpty(text))
            {
                ToolTip.Builder builder;
                var parentContent = control.RootView;

                var position = TooltipEffect.GetPosition(Element);
                switch (position)
                {
                    case TooltipPosition.Top:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionAbove);
                        break;
                    case TooltipPosition.Left:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionLeftTo);
                        break;
                    case TooltipPosition.Right:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionRightTo);
                        break;
                    default:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionBelow);
                        break;
                }

                builder.SetAlign(ToolTip.AlignLeft);
                builder.SetBackgroundColor(TooltipEffect.GetBackgroundColor(Element).ToAndroid());
                builder.SetTextColor(TooltipEffect.GetTextColor(Element).ToAndroid());

                _toolTipView = builder.Build();
                _toolTipsManager?.Show(_toolTipView);
            }
        }

        class TipListener : Java.Lang.Object, ITipListener
        {
            public void OnTipDismissed(Android.Views.View p0, int p1, bool p2)
            {

            }
        }
    }
}