using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaDevelopers.Controls
{
    /// <summary>
    /// Badge attached property. Apply to any control to show a badge overlay.
    /// Usage: Badge.SetIsShow(myButton, true); Badge.SetText(myButton, "5");
    /// </summary>
    public class Badge : TemplatedControl
    {
        public static readonly AttachedProperty<string> TextProperty =
            AvaloniaProperty.RegisterAttached<Badge, Control, string>("Text");

        public static readonly AttachedProperty<double> BadgeFontSizeProperty =
            AvaloniaProperty.RegisterAttached<Badge, Control, double>("BadgeFontSize", 10);

        public static readonly AttachedProperty<bool> IsShowProperty =
            AvaloniaProperty.RegisterAttached<Badge, Control, bool>("IsShow");

        public static readonly AttachedProperty<double> HorizontalOffsetProperty =
            AvaloniaProperty.RegisterAttached<Badge, Control, double>("HorizontalOffset");

        public static readonly AttachedProperty<double> VerticalOffsetProperty =
            AvaloniaProperty.RegisterAttached<Badge, Control, double>("VerticalOffset");

        public static string GetText(Control element) => element.GetValue(TextProperty);
        public static void SetText(Control element, string value) => element.SetValue(TextProperty, value);

        public static double GetBadgeFontSize(Control element) => element.GetValue(BadgeFontSizeProperty);
        public static void SetBadgeFontSize(Control element, double value) => element.SetValue(BadgeFontSizeProperty, value);

        public static bool GetIsShow(Control element) => element.GetValue(IsShowProperty);
        public static void SetIsShow(Control element, bool value) => element.SetValue(IsShowProperty, value);

        public static double GetHorizontalOffset(Control element) => element.GetValue(HorizontalOffsetProperty);
        public static void SetHorizontalOffset(Control element, double value) => element.SetValue(HorizontalOffsetProperty, value);

        public static double GetVerticalOffset(Control element) => element.GetValue(VerticalOffsetProperty);
        public static void SetVerticalOffset(Control element, double value) => element.SetValue(VerticalOffsetProperty, value);

        static Badge()
        {
            IsShowProperty.Changed.AddClassHandler<Control>(OnIsShowChanged);
            TextProperty.Changed.AddClassHandler<Control>(OnTextChanged);
        }

        private static void OnIsShowChanged(Control control, AvaloniaPropertyChangedEventArgs e)
        {
            // Badge visibility is handled via attached property binding in XAML
        }

        private static void OnTextChanged(Control control, AvaloniaPropertyChangedEventArgs e)
        {
            // Text change - re-evaluate badge display
        }
    }

    /// <summary>
    /// BadgeControl - a panel that can display a badge overlay on its child.
    /// Use this when you need a self-contained badge rather than attached properties.
    /// </summary>
    public class BadgeControl : Panel
    {
        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<BadgeControl, string>(nameof(Text));

        public static readonly StyledProperty<bool> IsDotProperty =
            AvaloniaProperty.Register<BadgeControl, bool>(nameof(IsDot));

        public static readonly StyledProperty<IBrush> BadgeBackgroundProperty =
            AvaloniaProperty.Register<BadgeControl, IBrush>(nameof(BadgeBackground));

        public static readonly StyledProperty<IBrush> BadgeForegroundProperty =
            AvaloniaProperty.Register<BadgeControl, IBrush>(nameof(BadgeForeground));

        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<BadgeControl, CornerRadius>(nameof(CornerRadius), new CornerRadius(999));

        static BadgeControl()
        {
            AffectsArrange<BadgeControl>(TextProperty, IsDotProperty);
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

        public CornerRadius CornerRadius
        {
            get => GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var child in Children)
            {
                child.Measure(availableSize);
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0) return finalSize;

            var mainChild = Children[0];
            mainChild.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));

            if (Children.Count > 1)
            {
                var badge = Children[1];
                var badgeSize = Math.Max(badge.DesiredSize.Width, badge.DesiredSize.Height);
                var badgeRect = new Rect(finalSize.Width - badgeSize / 2, -badgeSize / 2, badgeSize, badgeSize);
                badge.Arrange(badgeRect);
            }

            return finalSize;
        }
    }
}
