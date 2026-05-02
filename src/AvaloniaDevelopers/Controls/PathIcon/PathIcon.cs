using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaDevelopers.Controls
{
    [PseudoClasses(":default")]
    public class PathIcon : TemplatedControl
    {
        public static readonly StyledProperty<PackIconKind> KindProperty =
            AvaloniaProperty.Register<PathIcon, PackIconKind>(nameof(Kind));

        public static readonly StyledProperty<StreamGeometry?> DataProperty =
            AvaloniaProperty.Register<PathIcon, StreamGeometry?>(nameof(Data));

        static PathIcon()
        {
            KindProperty.Changed.AddClassHandler<PathIcon>(OnKindChanged);
            AffectsRender<PathIcon>(DataProperty);
        }

        public PackIconKind Kind
        {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public StreamGeometry? Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        private static void OnKindChanged(PathIcon icon, AvaloniaPropertyChangedEventArgs e)
        {
            var kind = (PackIconKind)e.NewValue!;
            var resourceName = $"WD.{kind}Geometry";

            if (icon.FindResource(resourceName) is StreamGeometry geometry)
            {
                icon.Data = geometry;
            }
            else
            {
                icon.Data = null;
            }
        }
    }
}
