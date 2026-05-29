using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace WPFDevelopers.Demo
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            //ScrollViewer.AllowAutoHideProperty.OverrideDefaultValue(typeof(ScrollViewer), false);
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                //desktop.MainWindow = new Window1();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
