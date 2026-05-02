using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System.Windows.Input;

namespace AvaloniaDevelopers.Controls
{
    public class Tag : ContentControl
    {
        public static readonly StyledProperty<bool> IsCloseProperty =
            AvaloniaProperty.Register<Tag, bool>(nameof(IsClose), true);

        public static readonly StyledProperty<ICommand?> CloseCommandProperty =
            AvaloniaProperty.Register<Tag, ICommand?>(nameof(CloseCommand));

        public static readonly RoutedEvent<RoutedEventArgs> CloseEvent =
            RoutedEvent.Register<Tag, RoutedEventArgs>(nameof(Close), RoutingStrategies.Bubble);

        private Button? _closeButton;

        static Tag()
        {
            AffectsMeasure<Tag>(IsCloseProperty);
        }

        public bool IsClose
        {
            get => GetValue(IsCloseProperty);
            set => SetValue(IsCloseProperty, value);
        }

        public ICommand? CloseCommand
        {
            get => GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        public event EventHandler<RoutedEventArgs> Close
        {
            add => AddHandler(CloseEvent, value);
            remove => RemoveHandler(CloseEvent, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            if (_closeButton != null)
                _closeButton.Click -= OnCloseButtonClick;

            _closeButton = e.NameScope.Find<Button>("PART_CloseButton");

            if (_closeButton != null)
                _closeButton.Click += OnCloseButtonClick;
        }

        private void OnCloseButtonClick(object? sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CloseEvent, this));
            CloseCommand?.Execute(null);
        }
    }
}
