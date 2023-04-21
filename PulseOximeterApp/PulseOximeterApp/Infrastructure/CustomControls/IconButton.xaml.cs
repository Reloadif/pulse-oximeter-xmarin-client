using System.Windows.Input;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Infrastructure.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconButton : ContentView
    {
        #region BindableProperty
        public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(IconButton), default(Thickness));
        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(IconButton), Color.Default);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(IconButton), -1.0f);
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(IconButton));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(IconButton));
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(IconButton));
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(IconButton));
        public static readonly BindableProperty IconFontSizeProperty = BindableProperty.Create(nameof(IconFontSize), typeof(double), typeof(IconButton), -1.0);
        public static readonly BindableProperty TextFontSizeProperty = BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(IconButton), -1.0);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IconButton), null, propertyChanged: CommandPropertyChanged);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(IconButton), null);

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public double IconFontSize
        {
            get => (double)GetValue(IconFontSizeProperty);
            set => SetValue(IconFontSizeProperty, value);
        }
        public double TextFontSize
        {
            get => (double)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(newValue is Command)) return;

            var iconButton = bindable as IconButton;
            var command = newValue as ICommand;

            iconButton.ChangeEnabledState(command.CanExecute(iconButton.CommandParameter));
            command.CanExecuteChanged += (sender, args) => IconButton_CanExecuteChanged(sender, args, bindable);
        }

        private static void IconButton_CanExecuteChanged(object sender, EventArgs e, BindableObject bindable)
        {
            var iconButton = bindable as IconButton;
            iconButton.ChangeEnabledState((sender as ICommand).CanExecute(iconButton.CommandParameter));
        }

        private void ChangeEnabledState(bool state) => VisualStateManager.GoToState(this, state ? "Normal" : "Disabled");
        #endregion

        public event EventHandler Clicked;

        public IconButton()
        {
            InitializeComponent();

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this, EventArgs.Empty);
                    if (Command != null)
                    {
                        if (Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);
                    }
                })
            });

            XamlWrapperFrame.SetBinding(Frame.PaddingProperty, new Binding("Padding", source: this));
            XamlWrapperFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("BackgroundColor", source: this));
            XamlWrapperFrame.SetBinding(Frame.CornerRadiusProperty, new Binding("CornerRadius", source: this));
            XamlWrapperFrame.SetBinding(Frame.HasShadowProperty, new Binding("HasShadow", source: this));

            XamlIcon.SetBinding(IconLabel.TextColorProperty, new Binding("TextColor", source: this));
            XamlIcon.SetBinding(IconLabel.TextIconProperty, new Binding("Icon", source: this));
            XamlIcon.SetBinding(IconLabel.FontSizeProperty, new Binding("IconFontSize", source: this));

            XamlText.SetBinding(Label.TextColorProperty, new Binding("TextColor", source: this));
            XamlText.SetBinding(Label.TextProperty, new Binding("Text", source: this));
            XamlText.SetBinding(Label.FontSizeProperty, new Binding("TextFontSize", source: this));
        }
    }
}