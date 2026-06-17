using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using System.Collections.Generic;
using WPFDevelopers.Avalonia;
using WPFDevelopers.Avalonia.Controls;

namespace WPFDevelopers.Demo
{
    public partial class MainWindow : Window
    {
        private bool _isDark = false;
        private List<ColorOption> _colorOptions;
        public MainWindow()
        {
            InitializeComponent();

            if (Application.Current is { } app)
            {
                var trayIcons = TrayIcon.GetIcons(app);
                if (trayIcons == null || trayIcons.Count == 0)
                {
                    var showItem = new NativeMenuItem("Show");
                    var exitItem = new NativeMenuItem("Exit");
                    showItem.Click += TrayShow_Click;
                    exitItem.Click += TrayExit_Click;

                    var tray = new TrayIcon
                    {
                        Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://WPFDevelopers.Demo/Assets/WPFDevelopers.ico"))),
                        Menu = new NativeMenu
                        {
                            showItem,
                            exitItem
                        }
                    };
                    TrayIcon.SetIcons(app, [tray]);
                }
            }

            InitializeColorOptions();
            var themeToggle = this.FindControl<Button>("ThemeToggle");
            if (themeToggle != null)
            {
                themeToggle.Click += OnThemeToggleClick;
            }

            var toastInfoBtn = this.FindControl<Button>("ToastInfoBtn");
            if (toastInfoBtn != null) toastInfoBtn.Click += (_, _) => Toast.Push("This is an info toast message.", ToastIcon.Info);

            var toastSuccessBtn = this.FindControl<Button>("ToastSuccessBtn");
            if (toastSuccessBtn != null) toastSuccessBtn.Click += (_, _) => Toast.Push("Operation completed successfully!", ToastIcon.Success);

            var toastWarningBtn = this.FindControl<Button>("ToastWarningBtn");
            if (toastWarningBtn != null) toastWarningBtn.Click += (_, _) => Toast.Push("Warning: please check your input.", ToastIcon.Warning);

            var toastErrorBtn = this.FindControl<Button>("ToastErrorBtn");
            if (toastErrorBtn != null) toastErrorBtn.Click += (_, _) => Toast.Push("An error occurred. Please try again.", ToastIcon.Error);

            var toastClearBtn = this.FindControl<Button>("ToastClearBtn");
            if (toastClearBtn != null) toastClearBtn.Click += (_, _) => Toast.Clear();

            var msgBoxInfoBtn = this.FindControl<Button>("MsgBoxInfoBtn");
            if (msgBoxInfoBtn != null) msgBoxInfoBtn.Click += (_, _) => MessageBox.Show("This is an information message.", "Information", MessageBoxImage.Information, this);

            var msgBoxWarningBtn = this.FindControl<Button>("MsgBoxWarningBtn");
            if (msgBoxWarningBtn != null) msgBoxWarningBtn.Click += (_, _) => MessageBox.Show("This is a warning message.", "Warning", MessageBoxImage.Warning, this);

            var msgBoxErrorBtn = this.FindControl<Button>("MsgBoxErrorBtn");
            if (msgBoxErrorBtn != null) msgBoxErrorBtn.Click += (_, _) => MessageBox.Show("An error has occurred.", "Error", MessageBoxImage.Error, this);

            var msgBoxQuestionBtn = this.FindControl<Button>("MsgBoxQuestionBtn");
            if (msgBoxQuestionBtn != null) msgBoxQuestionBtn.Click += (_, _) => MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question, this);

            var msgBoxYesNoCancelBtn = this.FindControl<Button>("MsgBoxYesNoCancelBtn");
            if (msgBoxYesNoCancelBtn != null) msgBoxYesNoCancelBtn.Click += async (_, _) =>
            {
                var result = await MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, this);
                if (result == MessageBoxResult.Yes)
                {
                    Toast.Push($"Result: {result}", ToastIcon.Info);
                }
            };

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

            var autoCompleteBox = this.FindControl<AutoCompleteBox>("DemoAutoCompleteBox");
            if (autoCompleteBox != null)
            {
                autoCompleteBox.ItemsSource = new List<string>
                {
                    "Avalonia", "AvaloniaUI", "Button", "CheckBox",
                    "ComboBox", "DataGrid", "Expander", "ListBox", "Menu",
                    "NumericUpDown", "ProgressBar", "RadioButton", "Slider",
                    "TabControl", "TextBox", "TreeView", "SplitView", "TimePicker",
                    "WPFDevelopers","WPF","yanjinhua","WPF开发者"
                };
            }

            var repeatValueText = this.FindControl<TextBlock>("RepeatValueText");
            int repeatCounter = 0;
            var repeatIncrementBtn = this.FindControl<RepeatButton>("RepeatIncrementBtn");
            if (repeatIncrementBtn != null && repeatValueText != null)
                repeatIncrementBtn.Click += (_, _) => { repeatCounter++; repeatValueText.Text = repeatCounter.ToString(); };

            var repeatDecrementBtn = this.FindControl<RepeatButton>("RepeatDecrementBtn");
            if (repeatDecrementBtn != null && repeatValueText != null)
                repeatDecrementBtn.Click += (_, _) => { repeatCounter--; repeatValueText.Text = repeatCounter.ToString(); };

            var splitViewToggle = this.FindControl<Button>("SplitViewToggleBtn");
            var demoSplitView = this.FindControl<SplitView>("DemoSplitView");
            if (splitViewToggle != null && demoSplitView != null)
                splitViewToggle.Click += (_, _) => demoSplitView.IsPaneOpen = !demoSplitView.IsPaneOpen;

            var overlayRadio = this.FindControl<RadioButton>("SplitViewOverlayBtn");
            var inlineRadio = this.FindControl<RadioButton>("SplitViewInlineBtn");
            var compactRadio = this.FindControl<RadioButton>("SplitViewCompactBtn");
            if (demoSplitView != null)
            {
                if (overlayRadio != null) overlayRadio.IsCheckedChanged += (_, _) => { if (overlayRadio.IsChecked == true) demoSplitView.DisplayMode = SplitViewDisplayMode.Overlay; };
                if (inlineRadio != null) inlineRadio.IsCheckedChanged += (_, _) => { if (inlineRadio.IsChecked == true) demoSplitView.DisplayMode = SplitViewDisplayMode.Inline; };
                if (compactRadio != null) compactRadio.IsCheckedChanged += (_, _) => { if (compactRadio.IsChecked == true) demoSplitView.DisplayMode = SplitViewDisplayMode.CompactInline; };
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

        private void InitializeColorOptions()
        {
            _colorOptions = new List<ColorOption>
            {
                new ColorOption { Name = "玫瑰红", Color = new SolidColorBrush(Color.Parse("#FF4D6D")), ColorCode = "#FF4D6D" },
                new ColorOption { Name = "珊瑚橙", Color = new SolidColorBrush(Color.Parse("#FF6D3A")), ColorCode = "#FF6D3A" },
                new ColorOption { Name = "向日葵黄", Color = new SolidColorBrush(Color.Parse("#FFD166")), ColorCode = "#FFD166" },
                new ColorOption { Name = "草绿色", Color = new SolidColorBrush(Color.Parse("#06D6A0")), ColorCode = "#06D6A0" },
                new ColorOption { Name = "天空蓝", Color = new SolidColorBrush(Color.Parse("#118AB2")), ColorCode = "#118AB2" },
                new ColorOption { Name = "薰衣草紫", Color = new SolidColorBrush(Color.Parse("#9B5DE5")), ColorCode = "#9B5DE5" },
                new ColorOption { Name = "樱花粉", Color = new SolidColorBrush(Color.Parse("#FFB7B2")), ColorCode = "#FFB7B2" },
                new ColorOption { Name = "薄荷绿", Color = new SolidColorBrush(Color.Parse("#A7E0E0")), ColorCode = "#A7E0E0" }
            };

            ColorItemsControl.ItemsSource = _colorOptions;
        }

        private void OnColorRadioButtonChecked(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && rb.IsChecked == true && rb.Tag is ColorOption selectedColor)
            {
                ((Resources)Application.Current.Resources).Color = Color.Parse(selectedColor.ColorCode);
            }
        }

        private void TrayShow_Click(object? sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void TrayExit_Click(object? sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        
    }
    public class ColorOption
    {
        public string Name { get; set; }
        public IBrush Color { get; set; }
        public string ColorCode { get; set; }
    }

}
