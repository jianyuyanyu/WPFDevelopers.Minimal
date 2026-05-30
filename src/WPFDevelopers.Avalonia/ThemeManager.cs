using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace WPFDevelopers.Avalonia
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
            var defaultColor = Color.Parse("#409EFF");
            if (_primaryColor == defaultColor)
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

            var lightUri = new Uri("avares://WPFDevelopers.Avalonia/Themes/Basic/Light.Color.axaml");
            var darkUri = new Uri("avares://WPFDevelopers.Avalonia/Themes/Basic/Dark.Color.axaml");
            var newUri = _theme == ThemeType.Light ? lightUri : darkUri;
            if (_themeColorDictionary != null)
            {
                appResources.MergedDictionaries.Remove(_themeColorDictionary);
            }
            try
            {
                var colorDict = AvaloniaXamlLoader.Load(newUri) as IResourceDictionary;
                if (colorDict == null) return;

                _themeColorDictionary = new ResourceDictionary();
                foreach (var kvp in colorDict)
                {
                    _themeColorDictionary[kvp.Key] = kvp.Value;
                }

                var defaultColor = Color.Parse("#409EFF");
                if (_primaryColor != defaultColor)
                {
                    SetResource(_themeColorDictionary, "WD.PrimaryColor", _primaryColor);
                    SetResource(_themeColorDictionary, "WD.WindowBorderColor", _primaryColor);
                    SetResource(_themeColorDictionary, "WD.PrimaryMouseOverColor", ComputeMouseOverColor(_primaryColor, _theme));
                }

                appResources.MergedDictionaries.Add(_themeColorDictionary);
            }
            catch { }

            ThemeChanged?.Invoke(_theme);
        }

        private void UpdatePrimaryColor()
        {
            if (Application.Current == null) return;
            var target = _themeColorDictionary ?? (IResourceDictionary)Application.Current.Resources;
            target["WD.PrimaryColor"] = _primaryColor;
            target["WD.WindowBorderColor"] = _primaryColor;
            target["WD.PrimaryMouseOverColor"] = ComputeMouseOverColor(_primaryColor, _theme);
        }

        private static Color ComputeMouseOverColor(Color color, ThemeType theme)
        {
            var bg = theme == ThemeType.Dark ? Color.Parse("#323232") : Color.Parse("#FFFFFF");
            return Color.FromArgb((byte)0x1A,
                BlendAlpha(color.R, bg.R, color.A),
                BlendAlpha(color.G, bg.G, color.A),
                BlendAlpha(color.B, bg.B, color.A));
        }

        private static byte BlendAlpha(byte fg, byte bg, byte alpha)
        {
            return (byte)(bg + (fg - bg) * alpha / 255);
        }

        private static void SetResource(IResourceDictionary resources, string key, Color color)
        {
            resources[key] = color;
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
