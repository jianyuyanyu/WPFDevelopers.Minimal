using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AvaloniaDevelopers.Controls
{
    public abstract class LoadingBase : TemplatedControl
    {
        public static readonly StyledProperty<bool> IsLoadingProperty =
            AvaloniaProperty.Register<LoadingBase, bool>(nameof(IsLoading), true);

        public static readonly StyledProperty<double> DotSizeProperty =
            AvaloniaProperty.Register<LoadingBase, double>(nameof(DotSize), 10);

        public bool IsLoading
        {
            get => GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public double DotSize
        {
            get => GetValue(DotSizeProperty);
            set => SetValue(DotSizeProperty, value);
        }
    }
}
