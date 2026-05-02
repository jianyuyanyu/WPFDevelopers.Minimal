using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace WidgetsDesign.Avalonia
{
    public static class WidgetsDesignExtensions
    {
        /// <summary>
        /// Initialize WidgetsDesign theme resources.
        /// Call this in App.OnFrameworkInitializationCompleted before loading styles.
        /// </summary>
        public static AppBuilder UseWidgetsDesign(this AppBuilder builder)
        {
            ThemeManager.Instance.Initialize();
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
