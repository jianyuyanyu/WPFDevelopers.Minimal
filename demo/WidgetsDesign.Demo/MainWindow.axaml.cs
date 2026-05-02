using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using WidgetsDesign.Avalonia;

namespace WidgetsDesign.Demo
{
    public partial class MainWindow : Window
    {
        private bool _isDark = false;

        public MainWindow()
        {
            InitializeComponent();

            var themeToggle = this.FindControl<Button>("ThemeToggle");
            if (themeToggle != null)
            {
                themeToggle.Click += OnThemeToggleClick;
            }
        }

        private void OnThemeToggleClick(object? sender, RoutedEventArgs e)
        {
            _isDark = !_isDark;
            var themeToggle = sender as Button;
            if (themeToggle != null)
            {
                themeToggle.Content = _isDark ? "Switch to Light" : "Switch to Dark";
            }
            Application.Current?.SetTheme(_isDark ? ThemeType.Dark : ThemeType.Light);
        }

        private void Tag_Close(object? sender, RoutedEventArgs e)
        {

        }
    }
}
