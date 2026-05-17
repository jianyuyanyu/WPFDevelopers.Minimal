using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace WidgetDesign.Avalonia.Controls
{
    [PseudoClasses(":default")]
    public class PathIconEx : TemplatedControl
    {
        public static readonly StyledProperty<PackIconKind> KindProperty =
            AvaloniaProperty.Register<PathIconEx, PackIconKind>(nameof(Kind));

        public static readonly StyledProperty<StreamGeometry?> DataProperty =
            AvaloniaProperty.Register<PathIconEx, StreamGeometry?>(nameof(Data));

        static PathIconEx()
        {
            KindProperty.Changed.AddClassHandler<PathIconEx>(OnKindChanged);
            AffectsRender<PathIconEx>(DataProperty);
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

        private static void OnKindChanged(PathIconEx icon, AvaloniaPropertyChangedEventArgs e)
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

            if (this.FindResource(resourceName) is StreamGeometry geometry)
            {
                Data = geometry;
                return;
            }

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
