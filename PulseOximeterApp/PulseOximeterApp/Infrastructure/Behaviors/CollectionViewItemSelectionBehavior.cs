using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.Behaviors
{
    public class CollectionViewItemSelectionBehavior : Behavior<CollectionView>
    {
        #region BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CollectionViewItemSelectionBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        #endregion

        protected override void OnAttachedTo(CollectionView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.BindingContextChanged += BindableBindingContextChanged;
            bindable.SelectionChanged += BindableSelectionChanged;
        }

        protected override void OnDetachingFrom(CollectionView bindable)
        {
            bindable.SelectionChanged -= BindableSelectionChanged;
            bindable.BindingContextChanged -= BindableBindingContextChanged;

            base.OnDetachingFrom(bindable);
        }

        private void BindableBindingContextChanged(object sender, EventArgs e)
        {
            CollectionView collection = sender as CollectionView;
            BindingContext = collection?.BindingContext;
        }

        private void BindableSelectionChanged(object sender, EventArgs e)
        {
            CollectionView collection = sender as CollectionView;
            if (collection.SelectedItem != null)
            {
                Command?.Execute(collection.SelectedItem);
                collection.SelectedItem = null;
            }
        }
    }
}
