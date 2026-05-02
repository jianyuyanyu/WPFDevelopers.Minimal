using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace AvaloniaDevelopers
{
    public static class AvaloniaDevelopersExtensions
    {
        /// <summary>
        /// Initialize AvaloniaDevelopers theme resources.
        /// Call this in App.OnFrameworkInitializationCompleted before loading styles.
        /// </summary>
        public static void UseAvaloniaDevelopers(this AppBuilder builder)
        {
            ThemeManager.Instance.Initialize();
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
