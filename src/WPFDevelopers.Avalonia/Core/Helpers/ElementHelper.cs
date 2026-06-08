using Avalonia;
using Avalonia.Controls;

namespace WPFDevelopers.Avalonia.Helpers
{
    public static class ElementHelper
    {
        private static readonly AttachedProperty<CornerRadius> _roundCornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Control, CornerRadius>("RoundCornerRadius", typeof(ElementHelper));

        public static readonly AttachedProperty<bool> IsRoundProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("IsRound", typeof(ElementHelper));

        static ElementHelper()
        {
            IsRoundProperty.Changed.AddClassHandler<Control>(OnIsRoundChanged);
        }

        public static bool GetIsRound(Control element) => element.GetValue(IsRoundProperty);
        public static void SetIsRound(Control element, bool value) => element.SetValue(IsRoundProperty, value);

        public static CornerRadius GetRoundCornerRadius(Control element) => element.GetValue(_roundCornerRadiusProperty);
        public static void SetRoundCornerRadius(Control element, CornerRadius value) => element.SetValue(_roundCornerRadiusProperty, value);

        private static void OnIsRoundChanged(Control control, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is not bool b)
                return;

            if (b)
            {
                control.Classes.Add("round");
                SetRoundCornerRadius(control, new CornerRadius(999));
            }
            else
            {
                control.Classes.Remove("round");
                SetRoundCornerRadius(control, default);
            }
        }
    }
}
