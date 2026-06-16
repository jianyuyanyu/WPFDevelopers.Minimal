using Avalonia;
using Avalonia.Media;

namespace WPFDevelopers.Avalonia
{
    public static class WPFDevelopersExtensions
    {
        public static AppBuilder UseWPFDevelopers(this AppBuilder builder)
        {
            builder.AfterSetup(_ =>
            {
                ThemeManager.Instance.Initialize();
            });
            return builder;
        }
      
        public static void SetTheme(this Application app, ThemeType themeType)
        {
            ThemeManager.Instance.Theme = themeType;
        }

        public static void SetPrimaryColor(this Application app, Color color)
        {
            ThemeManager.Instance.PrimaryColor = color;
        }
    }
}
