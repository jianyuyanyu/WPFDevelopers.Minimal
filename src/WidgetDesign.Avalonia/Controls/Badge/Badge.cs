using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace WidgetDesign.Avalonia.Controls
{
    public static class Badge
    {
        public static readonly AttachedProperty<string?> BadgeTextProperty =
            AvaloniaProperty.RegisterAttached<Control, string?>("BadgeText", typeof(Badge));

        public static readonly AttachedProperty<bool> IsDotProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("IsDot", typeof(Badge));

        public static readonly AttachedProperty<IBrush?> BadgeBackgroundProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush?>("BadgeBackground", typeof(Badge));

        public static readonly AttachedProperty<IBrush?> BadgeForegroundProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush?>("BadgeForeground", typeof(Badge));

        public static readonly AttachedProperty<double> BadgeFontSizeProperty =
            AvaloniaProperty.RegisterAttached<Control, double>("BadgeFontSize", typeof(Badge), 10);

        private static readonly AttachedProperty<BadgeOverlay?> BadgeOverlayDataProperty =
            AvaloniaProperty.RegisterAttached<Control, BadgeOverlay?>("BadgeOverlayData", typeof(Badge));

        static Badge()
        {
            BadgeTextProperty.Changed.AddClassHandler<Control>((c, _) => Dispatcher.UIThread.Post(() => UpdateBadge(c)));
            IsDotProperty.Changed.AddClassHandler<Control>((c, _) => Dispatcher.UIThread.Post(() => UpdateBadge(c)));
            BadgeBackgroundProperty.Changed.AddClassHandler<Control>((c, _) => Dispatcher.UIThread.Post(() => UpdateBadge(c)));
            BadgeForegroundProperty.Changed.AddClassHandler<Control>((c, _) => Dispatcher.UIThread.Post(() => UpdateBadge(c)));
            BadgeFontSizeProperty.Changed.AddClassHandler<Control>((c, _) => Dispatcher.UIThread.Post(() => UpdateBadge(c)));
        }

        public static string? GetBadgeText(Control element) => element.GetValue(BadgeTextProperty);
        public static void SetBadgeText(Control element, string? value) => element.SetValue(BadgeTextProperty, value);

        public static bool GetIsDot(Control element) => element.GetValue(IsDotProperty);
        public static void SetIsDot(Control element, bool value) => element.SetValue(IsDotProperty, value);

        public static IBrush? GetBadgeBackground(Control element) => element.GetValue(BadgeBackgroundProperty);
        public static void SetBadgeBackground(Control element, IBrush? value) => element.SetValue(BadgeBackgroundProperty, value);

        public static IBrush? GetBadgeForeground(Control element) => element.GetValue(BadgeForegroundProperty);
        public static void SetBadgeForeground(Control element, IBrush? value) => element.SetValue(BadgeForegroundProperty, value);

        public static double GetBadgeFontSize(Control element) => element.GetValue(BadgeFontSizeProperty);
        public static void SetBadgeFontSize(Control element, double value) => element.SetValue(BadgeFontSizeProperty, value);

        private static void UpdateBadge(Control control)
        {
            var text = GetBadgeText(control);
            var isDot = GetIsDot(control);
            if (isDot || !string.IsNullOrEmpty(text))
                ShowBadge(control, text, isDot);
            else
                HideBadge(control);
        }

        private static void ShowBadge(Control control, string? text, bool isDot)
        {
            var overlay = control.GetValue(BadgeOverlayDataProperty);
            if (overlay != null)
            {
                overlay.UpdateBadge(text, isDot);
                return;
            }

            var bg = GetBadgeBackground(control) ?? control.FindResource("WD.DangerBrush") as IBrush ?? Brushes.Red;
            var fg = GetBadgeForeground(control) ?? Brushes.White;
            var fontSize = GetBadgeFontSize(control);

            var badgeOverlay = new BadgeOverlay(control, text, isDot, bg, fg, fontSize);
            control.SetValue(BadgeOverlayDataProperty, badgeOverlay);

            TryInsertOverlay(control, badgeOverlay);
        }

        private static void HideBadge(Control control)
        {
            var overlay = control.GetValue(BadgeOverlayDataProperty);
            if (overlay == null) return;

            control.LayoutUpdated -= overlay.OnLayoutUpdated;

            if (overlay.WrapperGrid != null)
                RestoreWrappedChild(overlay.WrapperGrid, control);

            overlay.Dispose();
            control.SetValue(BadgeOverlayDataProperty, null);
        }

        private static void TryInsertOverlay(Control control, BadgeOverlay badgeOverlay)
        {
            Visual? current = control.GetVisualParent();
            Control? childToReplace = control;

            while (current != null)
            {
                if (current is Panel panel)
                {
                    var index = panel.Children.IndexOf(childToReplace);
                    if (index < 0) break;
                    panel.Children.RemoveAt(index);
                    var grid = new Grid
                    {
                        Children = { control, badgeOverlay.BadgeElement },
                        Margin = control.Margin
                    };
                    control.Margin = new Thickness();
                    badgeOverlay.WrapperGrid = grid;
                    panel.Children.Insert(index, grid);
                    control.LayoutUpdated += badgeOverlay.OnLayoutUpdated;
                    Dispatcher.UIThread.Post(() => badgeOverlay.OnLayoutUpdated(control, EventArgs.Empty));
                    return;
                }

                if (current is ContentControl cc)
                {
                    if (!ReferenceEquals(cc.Content, childToReplace)) break;
                    var grid = new Grid
                    {
                        Children = { control, badgeOverlay.BadgeElement },
                        Margin = control.Margin
                    };
                    control.Margin = new Thickness();
                    badgeOverlay.WrapperGrid = grid;
                    cc.Content = grid;
                    control.LayoutUpdated += badgeOverlay.OnLayoutUpdated;
                    Dispatcher.UIThread.Post(() => badgeOverlay.OnLayoutUpdated(control, EventArgs.Empty));
                    return;
                }

                if (current is Decorator dec)
                {
                    if (!ReferenceEquals(dec.Child, childToReplace)) break;
                    var grid = new Grid
                    {
                        Children = { control, badgeOverlay.BadgeElement },
                        Margin = control.Margin
                    };
                    control.Margin = new Thickness();
                    badgeOverlay.WrapperGrid = grid;
                    dec.Child = grid;
                    control.LayoutUpdated += badgeOverlay.OnLayoutUpdated;
                    Dispatcher.UIThread.Post(() => badgeOverlay.OnLayoutUpdated(control, EventArgs.Empty));
                    return;
                }

                if (current is ContentPresenter cp)
                {
                    if (!ReferenceEquals(cp.Content, childToReplace)) break;
                    var grid = new Grid
                    {
                        Children = { control, badgeOverlay.BadgeElement },
                        Margin = control.Margin
                    };
                    control.Margin = new Thickness();
                    badgeOverlay.WrapperGrid = grid;
                    cp.Content = grid;
                    control.LayoutUpdated += badgeOverlay.OnLayoutUpdated;
                    Dispatcher.UIThread.Post(() => badgeOverlay.OnLayoutUpdated(control, EventArgs.Empty));
                    return;
                }

                childToReplace = (Control)current;
                current = current.GetVisualParent();
            }
        }

        private static void RestoreWrappedChild(Grid wrapperGrid, Control control)
        {
            wrapperGrid.Children.Remove(control);
            control.Margin = wrapperGrid.Margin;

            var parent = wrapperGrid.GetVisualParent();
            if (parent is Panel panel)
            {
                var index = panel.Children.IndexOf(wrapperGrid);
                panel.Children.RemoveAt(index);
                panel.Children.Insert(index, control);
            }
            else if (parent is ContentControl cc) cc.Content = control;
            else if (parent is Decorator dec) dec.Child = control;
            else if (parent is ContentPresenter cp) cp.Content = control;
        }

        private class BadgeOverlay
        {
            public Control BadgeElement;
            public Grid? WrapperGrid;
            private readonly Control _target;
            private readonly Border _badgeBorder;
            private readonly TextBlock _badgeText;
            private readonly Ellipse _dotEllipse;

            public BadgeOverlay(Control target, string? text, bool isDot, IBrush bg, IBrush fg, double fontSize)
            {
                _target = target;

                _badgeBorder = new Border
                {
                    Background = bg,
                    CornerRadius = new CornerRadius(999),
                    MinWidth = 16,
                    Padding = new Thickness(4, 0),
                    HorizontalAlignment = global::Avalonia.Layout.HorizontalAlignment.Right,
                    VerticalAlignment = global::Avalonia.Layout.VerticalAlignment.Top
                };

                _badgeText = new TextBlock
                {
                    Foreground = fg,
                    FontSize = fontSize,
                    HorizontalAlignment = global::Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = global::Avalonia.Layout.VerticalAlignment.Center,
                    Text = text ?? ""
                };

                _dotEllipse = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = bg,
                    HorizontalAlignment = global::Avalonia.Layout.HorizontalAlignment.Right,
                    VerticalAlignment = global::Avalonia.Layout.VerticalAlignment.Top,
                    IsVisible = isDot
                };

                _badgeBorder.Child = _badgeText;
                _badgeBorder.IsVisible = !isDot;

                var grid = new Grid
                {
                    HorizontalAlignment = global::Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = global::Avalonia.Layout.VerticalAlignment.Stretch
                };
                grid.Children.Add(_badgeBorder);
                grid.Children.Add(_dotEllipse);
                BadgeElement = grid;
            }

            public void UpdateBadge(string? text, bool isDot)
            {
                _badgeBorder.IsVisible = !isDot;
                _dotEllipse.IsVisible = isDot;
                if (!isDot)
                    _badgeText.Text = text ?? "";
            }

            public void OnLayoutUpdated(object? sender, System.EventArgs e)
            {
                if (_badgeBorder.Bounds.Width > 0 && _badgeBorder.Bounds.Height > 0)
                {
                    var w = _badgeBorder.Bounds.Width;
                    var h = _badgeBorder.Bounds.Height;
                    _badgeBorder.Margin = new Thickness(0, -h / 2, -w / 2, 0);
                }

                if (_dotEllipse.Bounds.Width > 0)
                {
                    _dotEllipse.Margin = new Thickness(0, -4, -4, 0);
                }
            }

            public void Dispose()
            {
                (BadgeElement as Grid)?.Children.Clear();
                _badgeBorder.Child = null;
            }
        }
    }
}
