using Avalonia;
using Avalonia.Media;

namespace WidgetDesign.Avalonia
{
    public static class WidgetDesignExtensions
    {
        /// <summary>
        /// Initialize WidgetDesign theme resources.
        /// Optional: if &lt;wd:Resources&gt; is used in App.axaml, this method is not needed.
        /// If not using Resources in XAML, call this after AvaloniaXamlLoader.Load(this) in App.Initialize().
        /// </summary>
        public static AppBuilder UseWidgetDesign(this AppBuilder builder)
        {
            builder.AfterSetup(_ =>
            {
                ThemeManager.Instance.Initialize();
            });
            return builder;
        }

        /// <summary>
        /// Switch theme to Light or Dark.
        /// </summary>
        public static void SetTheme(this Application app, ThemeType themeType)
        {
            ThemeManager.Instance.Theme = themeType;
        }

        /// <summary>
        /// Set the primary color dynamically.
        /// </summary>
        public static void SetPrimaryColor(this Application app, Color color)
        {
            ThemeManager.Instance.PrimaryColor = color;
        }
    }
}
