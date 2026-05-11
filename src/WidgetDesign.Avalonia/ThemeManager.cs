using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace WidgetDesign.Avalonia
{
    public enum ThemeType
    {
        Light,
        Dark
    }

    public delegate void ThemeChangedEventHandler(ThemeType themeType);

    public class ThemeManager
    {
        private static ThemeManager? _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();

        public event ThemeChangedEventHandler? ThemeChanged;

        private ThemeType _theme = ThemeType.Light;
        public ThemeType Theme
        {
            get => _theme;
            set
            {
                if (_theme == value) return;
                _theme = value;
                ApplyTheme();
            }
        }

        private Color _primaryColor = Color.Parse("#409EFF");
        public Color PrimaryColor
        {
            get => _primaryColor;
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                    UpdatePrimaryColor();
                }
            }
        }

        public IBrush? PrimaryBrush => GetResource<IBrush>("WD.PrimaryBrush");
        public IBrush? BackgroundBrush => GetResource<IBrush>("WD.BackgroundBrush");
        public IBrush? PrimaryTextBrush => GetResource<IBrush>("WD.PrimaryTextBrush");

        private ResourceDictionary? _themeColorDictionary;

        public void Initialize()
        {
            if (Application.Current == null) return;
            Application.Current.RequestedThemeVariant = _theme == ThemeType.Light
              ? ThemeVariant.Light
              : ThemeVariant.Dark;
            _primaryColor = GetResource<Color>("WD.PrimaryColor");
            ThemeChanged?.Invoke(_theme);
        }

        private void ApplyTheme()
        {
            if (Application.Current == null) return;

            Application.Current.RequestedThemeVariant = _theme == ThemeType.Light
                ? ThemeVariant.Light
                : ThemeVariant.Dark;

            var appResources = Application.Current.Resources;

            var lightUri = new Uri("avares://WidgetDesign.Avalonia/Themes/Basic/Light.Color.axaml");
            var darkUri = new Uri("avares://WidgetDesign.Avalonia/Themes/Basic/Dark.Color.axaml");
            var newUri = _theme == ThemeType.Light ? lightUri : darkUri;

            // Remove old theme dictionary
            if (_themeColorDictionary != null)
            {
                appResources.MergedDictionaries.Remove(_themeColorDictionary);
            }

            // Create a new dictionary for the new theme (new instance = forces DynamicResource re-eval)
            try
            {
                var colorDict = AvaloniaXamlLoader.Load(newUri) as IResourceDictionary;
                if (colorDict == null) return;

                _themeColorDictionary = new ResourceDictionary();
                foreach (var kvp in colorDict)
                {
                    _themeColorDictionary[kvp.Key] = kvp.Value;
                }
                appResources.MergedDictionaries.Add(_themeColorDictionary);
            }
            catch { }

            _primaryColor = GetResource<Color>("WD.PrimaryColor");
            ThemeChanged?.Invoke(_theme);
        }

        private void UpdatePrimaryColor()
        {
            if (Application.Current == null) return;
            var resources = Application.Current.Resources;
            SetResourceColor(resources, "WD.PrimaryColor", _primaryColor);
            SetResourceColor(resources, "WD.WindowBorderColor", _primaryColor);
        }

        private static void SetResourceColor(IResourceDictionary resources, string key, Color color)
        {
            resources[key] = color;
            var brushKey = key.Replace("Color", "Brush");
            resources[brushKey] = new SolidColorBrush(color);
        }

        private T? GetResource<T>(string key)
        {
            if (Application.Current == null) return default;
            if (Application.Current.Resources.TryGetResource(key, null, out var value) && value is T typed)
                return typed;
            return default;
        }
    }
}
