using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;

namespace WidgetsDesign.Avalonia
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

        private IResourceDictionary? _themeDictionary;
        private ResourceInclude? _colorInclude;

        public void Initialize()
        {
            if (Application.Current == null) return;

            var app = Application.Current;

            // Load main theme dictionary
            var themeUri = new Uri("avares://WidgetsDesign.Avalonia/Themes/Theme.axaml");
            var themeDict = AvaloniaXamlLoader.Load(themeUri) as IResourceDictionary;

            if (themeDict != null)
            {
                _themeDictionary = themeDict;
                app.Resources.MergedDictionaries.Add(_themeDictionary);
            }

            // Load light color dictionary by default
            ApplyTheme();

            _primaryColor = GetResource<Color>("WD.PrimaryColor");
        }

        private void ApplyTheme()
        {
            if (Application.Current == null || _themeDictionary == null) return;

            var lightUri = new Uri("avares://WidgetsDesign.Avalonia/Themes/Basic/Light.Color.axaml");
            var darkUri = new Uri("avares://WidgetsDesign.Avalonia/Themes/Basic/Dark.Color.axaml");

            // Remove old color include
            var dicts = _themeDictionary.MergedDictionaries;
            if (_colorInclude != null)
            {
                dicts.Remove(_colorInclude);
            }

            // Load new color dictionary
            var newUri = _theme == ThemeType.Light ? lightUri : darkUri;
            try
            {
                var colorDict = AvaloniaXamlLoader.Load(newUri) as IResourceDictionary;
                if (colorDict != null)
                {
                    _colorInclude = new ResourceInclude(newUri)
                    {
                        Source = newUri
                    };
                    // Just add the dictionary directly instead of ResourceInclude
                    dicts.Add(colorDict);
                }
            }
            catch { }

            UpdatePrimaryColor();
            ThemeChanged?.Invoke(_theme);
        }

        private void UpdatePrimaryColor()
        {
            SetResourceColor("WD.PrimaryColor", _primaryColor);
            SetResourceColor("WD.WindowBorderColor", _primaryColor);
        }

        private void SetResourceColor(string key, Color color)
        {
            if (Application.Current == null) return;
            var resources = Application.Current.Resources;
            if (resources.ContainsKey(key))
            {
                resources[key] = color;
                var brushKey = key.Replace("Color", "Brush");
                resources[brushKey] = new SolidColorBrush(color);
            }
        }

        private T? GetResource<T>(string key)
        {
            if (Application.Current == null) return default;
            var resources = Application.Current.Resources;
            if (resources.TryGetValue(key, out var value) && value is T typed)
                return typed;
            return default;
        }
    }
}
