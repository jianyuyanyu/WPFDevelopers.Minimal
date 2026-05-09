using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace WidgetDesign.Avalonia.Controls
{
    /// <summary>
    /// Badge - displays a badge overlay on its content.
    /// Usage: &lt;controls:Badge Text="5"&gt;&lt;Button ... /&gt;&lt;/controls:Badge&gt;
    /// </summary>
    public class Badge : ContentControl
    {
        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<Badge, string>(nameof(Text));

        public static readonly StyledProperty<bool> IsDotProperty =
            AvaloniaProperty.Register<Badge, bool>(nameof(IsDot));

        public static readonly StyledProperty<IBrush> BadgeBackgroundProperty =
            AvaloniaProperty.Register<Badge, IBrush>(nameof(BadgeBackground));

        public static readonly StyledProperty<IBrush> BadgeForegroundProperty =
            AvaloniaProperty.Register<Badge, IBrush>(nameof(BadgeForeground));

        public static readonly StyledProperty<double> BadgeFontSizeProperty =
            AvaloniaProperty.Register<Badge, double>(nameof(BadgeFontSize), 10);

        static Badge()
        {
            IsDotProperty.Changed.AddClassHandler<Badge>((c, e) =>
                c.PseudoClasses.Set(":dot", (bool)e.NewValue!));
            CornerRadiusProperty.OverrideDefaultValue<Badge>(new CornerRadius(999));
        }

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsDot
        {
            get => GetValue(IsDotProperty);
            set => SetValue(IsDotProperty, value);
        }

        public IBrush BadgeBackground
        {
            get => GetValue(BadgeBackgroundProperty);
            set => SetValue(BadgeBackgroundProperty, value);
        }

        public IBrush BadgeForeground
        {
            get => GetValue(BadgeForegroundProperty);
            set => SetValue(BadgeForegroundProperty, value);
        }

        public double BadgeFontSize
        {
            get => GetValue(BadgeFontSizeProperty);
            set => SetValue(BadgeFontSizeProperty, value);
        }
    }
}
