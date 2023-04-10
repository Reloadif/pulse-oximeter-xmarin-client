using System;
using System.Linq;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.Behaviors
{
    public class IntegerValidationBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(
                nameof(MinValue),
                typeof(int),
                typeof(IntegerValidationBehavior),
                int.MinValue
            );

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
                nameof(MaxValue),
                typeof(int),
                typeof(IntegerValidationBehavior),
                int.MaxValue
            );

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.Unfocused += OnEntryUnfocus;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Unfocused -= OnEntryUnfocus;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryUnfocus(object sender, FocusEventArgs args)
        {
            Entry entry = sender as Entry;
            string text = entry.Text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                if (text.ToCharArray().All(x => char.IsDigit(x)))
                {
                    if (Convert.ToInt32(text) < MinValue)
                    {
                        entry.Text = MinValue.ToString();
                    }
                    else if (Convert.ToInt32(text) > MaxValue)
                    {
                        entry.Text = MaxValue.ToString();
                    }
                    else
                    {
                        entry.Text = text;
                    }
                }
                else
                {
                    entry.Text = text.Remove(text.Length - 1);
                }
            }
        }


    }
}
