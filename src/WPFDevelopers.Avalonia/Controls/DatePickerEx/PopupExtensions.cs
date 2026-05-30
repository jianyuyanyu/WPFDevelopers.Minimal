using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;

namespace WPFDevelopers.Avalonia.Controls;

public static class PopupExtensions
{
    public static readonly AttachedProperty<bool> ConstrainToScreenProperty =
        AvaloniaProperty.RegisterAttached<Popup, bool>("ConstrainToScreen", typeof(PopupExtensions));

    static PopupExtensions()
    {
        ConstrainToScreenProperty.Changed.AddClassHandler<Popup>(OnConstrainToScreenChanged);
    }

    public static void SetConstrainToScreen(Popup element, bool value) =>
        element.SetValue(ConstrainToScreenProperty, value);

    public static bool GetConstrainToScreen(Popup element) =>
        element.GetValue(ConstrainToScreenProperty);

    private static void OnConstrainToScreenChanged(Popup popup, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
        {
            popup.Opened += OnPopupOpened;
        }
        else
        {
            popup.Opened -= OnPopupOpened;
        }
    }

    private static async void OnPopupOpened(object? sender, System.EventArgs e)
    {
        if (sender is not Popup popup) return;

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            var window = TopLevel.GetTopLevel(popup) as Window;
            if (window is null) return;

            var screen = window.Screens.ScreenFromWindow(window);
            if (screen is null) return;

            var workingArea = screen.WorkingArea;
            var popupWidth = popup.DesiredSize.Width;
            if (popupWidth <= 0) popupWidth = 280;

            // Get the target element's position on screen
            var target = popup.PlacementTarget;
            if (target is null) target = popup.TemplatedParent as Control;
            if (target is null) return;

            var targetPoint = target.PointToScreen(new Point(0, 0));
            var targetLeft = targetPoint.X;
            var targetWidth = target.Bounds.Width;

            // For "Bottom" placement, popup is centered on target
            var popupLeft = targetLeft + (targetWidth / 2) - (popupWidth / 2);
            var popupRight = popupLeft + popupWidth;

            var offsetX = 0.0;
            if (popupLeft < workingArea.X)
                offsetX = workingArea.X - popupLeft;
            else if (popupRight > workingArea.X + workingArea.Width)
                offsetX = (workingArea.X + workingArea.Width) - popupRight;

            if (offsetX != 0)
            {
                popup.HorizontalOffset = offsetX + 10;
                popup.InvalidateMeasure();
            }
        }, DispatcherPriority.Background);
    }
}
