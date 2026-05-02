using Avalonia;
using Avalonia.Controls;

namespace WidgetsDesign.Avalonia.Helpers
{
    public static class ElementHelper
    {
        public static readonly AttachedProperty<double> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Control, double>("CornerRadius", typeof(ElementHelper));

        public static double GetCornerRadius(Control element) => element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(Control element, double value) => element.SetValue(CornerRadiusProperty, value);

        public static readonly AttachedProperty<bool> IsRoundProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("IsRound", typeof(ElementHelper));

        public static bool GetIsRound(Control element) => element.GetValue(IsRoundProperty);
        public static void SetIsRound(Control element, bool value) => element.SetValue(IsRoundProperty, value);
    }
}
