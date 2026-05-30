using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;

namespace WPFDevelopers.Avalonia
{
    public class Resources : ResourceDictionary
    {
        public static readonly StyledProperty<ThemeType> ThemeProperty =
            AvaloniaProperty.Register<Resources, ThemeType>(nameof(Theme), ThemeType.Light);

        public static readonly StyledProperty<Color> ColorProperty =
            AvaloniaProperty.Register<Resources, Color>(nameof(Color), Color.Parse("#409EFF"));

        public ThemeType Theme
        {
            get => GetValue(ThemeProperty);
            set => SetValue(ThemeProperty, value);
        }

        public Color Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        static Resources()
        {
            ThemeProperty.Changed.AddClassHandler<Resources>((r, _) =>
                Dispatcher.UIThread.Post(() => ThemeManager.Instance.Theme = r.Theme));

            ColorProperty.Changed.AddClassHandler<Resources>((r, _) =>
                Dispatcher.UIThread.Post(() => ThemeManager.Instance.PrimaryColor = r.Color));
        }

        public Resources()
        {
            var resourcesUri = new Uri("avares://WPFDevelopers.Avalonia/Themes/Resources.axaml");
            var dict = AvaloniaXamlLoader.Load(resourcesUri) as ResourceDictionary;
            if (dict != null && MergedDictionaries != null)
            {
                foreach (var merged in dict.MergedDictionaries)
                {
                    MergedDictionaries.Add(merged);
                }
            }

            Dispatcher.UIThread.Post(() =>
            {
                ThemeManager.Instance.Theme = Theme;
                ThemeManager.Instance.PrimaryColor = Color;
                ThemeManager.Instance.Initialize();
            });
        }
    }
}
