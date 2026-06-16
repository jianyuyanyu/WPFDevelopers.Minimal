using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;

namespace WPFDevelopers.Avalonia.Controls
{
    [TemplatePart("PART_HeaderBorder", typeof(Border))]
    [TemplatePart("PART_ContentBorder", typeof(Border))]
    public class GroupBox : ContentControl
    {
        public static readonly StyledProperty<object?> HeaderProperty =
            AvaloniaProperty.Register<GroupBox, object?>(nameof(Header));

        public static readonly StyledProperty<object?> HeaderTemplateProperty =
            AvaloniaProperty.Register<GroupBox, object?>(nameof(HeaderTemplate));

        static GroupBox()
        {
            AffectsMeasure<GroupBox>(HeaderProperty);
        }

        public object? Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public object? HeaderTemplate
        {
            get => GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }
    }
}
