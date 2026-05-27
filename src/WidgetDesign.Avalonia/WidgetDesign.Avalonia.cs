using Avalonia;
using Avalonia.Media;

namespace WidgetDesign.Avalonia
{
    public static class WidgetDesignExtensions
    {
        public static AppBuilder UseWidgetDesign(this AppBuilder builder)
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
