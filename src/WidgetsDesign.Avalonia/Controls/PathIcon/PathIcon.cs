using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace WidgetsDesign.Avalonia.Controls
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

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateGeometry();
        }

        private static void OnKindChanged(PathIcon icon, AvaloniaPropertyChangedEventArgs e)
        {
            icon.UpdateGeometry();
        }

        private void UpdateGeometry()
        {
            var kind = Kind;
            if (kind == PackIconKind.None)
            {
                Data = null;
                return;
            }

            var resourceName = $"WD.{kind}Geometry";

            // Search through the visual tree's resource dictionaries
            if (this.FindResource(resourceName) is StreamGeometry geometry)
            {
                Data = geometry;
                return;
            }

            // Fallback: search application resources
            if (Application.Current?.Resources.TryGetResource(resourceName, null, out var appResource) == true && appResource is StreamGeometry appGeometry)
            {
                Data = appGeometry;
            }
            else
            {
                Data = null;
            }
        }
    }
}
