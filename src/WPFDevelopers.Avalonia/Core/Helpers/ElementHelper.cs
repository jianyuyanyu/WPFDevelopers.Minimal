using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace WPFDevelopers.Avalonia.Helpers
{
    public static class ElementHelper
    {
        public static readonly AttachedProperty<bool> IsRoundProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("IsRound", typeof(ElementHelper));

        private static readonly Dictionary<Control, EventHandler> _layoutHandlers = [];
        private static readonly HashSet<Control> _applying = [];

        static ElementHelper()
        {
            IsRoundProperty.Changed.AddClassHandler<Control>(OnIsRoundChanged);
        }

        public static bool GetIsRound(Control element) => element.GetValue(IsRoundProperty);
        public static void SetIsRound(Control element, bool value) => element.SetValue(IsRoundProperty, value);

        private static void OnIsRoundChanged(Control control, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is not bool b)
                return;

            SetCornerRadius(control, b);
            ApplyRoundSizing(control, b);
        }

        private static void SetCornerRadius(Control control, bool isRound)
        {
            if (isRound)
            {
                control.Classes.Add("round");
            }
            else
            {
                control.Classes.Remove("round");
            }

            if (control is Button button)
            {
                if (isRound)
                {
                    button.SetValue(Button.CornerRadiusProperty, new CornerRadius(999));
                }
                else
                {
                    button.ClearValue(Button.CornerRadiusProperty);
                }
            }
        }

        private static void ApplyRoundSizing(Control control, bool isRound)
        {
            if (isRound)
            {
                if (_layoutHandlers.ContainsKey(control))
                    return;

                void handler(object? sender, EventArgs e)
                {
                    if (_applying.Contains(control))
                        return;

                    var w = control.Width;
                    var h = control.Height;
                    if (double.IsNaN(w) || double.IsNaN(h))
                    {
                        w = control.Bounds.Width;
                        h = control.Bounds.Height;
                    }

                    if (w <= 0 || h <= 0)
                        return;

                    var max = Math.Max(w, h);
                    if (Math.Abs(w - max) > 0.5 || Math.Abs(h - max) > 0.5)
                    {
                        try
                        {
                            _applying.Add(control);
                            control.Width = max;
                            control.Height = max;
                        }
                        finally
                        {
                            _applying.Remove(control);
                        }
                    }
                }

                _layoutHandlers[control] = handler;
                control.LayoutUpdated += handler;
            }
            else
            {
                if (_layoutHandlers.TryGetValue(control, out var handler))
                {
                    control.LayoutUpdated -= handler;
                    _layoutHandlers.Remove(control);
                }
            }
        }
    }
}
