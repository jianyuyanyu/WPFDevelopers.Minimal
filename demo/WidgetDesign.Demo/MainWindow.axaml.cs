using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using WidgetDesign.Avalonia;

namespace WidgetDesign.Demo
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

            var dataGrid = this.FindControl<DataGrid>("DemoDataGrid");
            if (dataGrid != null)
            {
                dataGrid.ItemsSource = new List<DemoItem>
                {
                    new() { Name = "Alice", Age = 28, IsActive = true, Role = "Admin" },
                    new() { Name = "Bob", Age = 34, IsActive = true, Role = "Editor" },
                    new() { Name = "Charlie", Age = 22, IsActive = false, Role = "Viewer" },
                    new() { Name = "Diana", Age = 41, IsActive = true, Role = "Admin" },
                    new() { Name = "Eve", Age = 30, IsActive = false, Role = "Editor" },
                };
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
