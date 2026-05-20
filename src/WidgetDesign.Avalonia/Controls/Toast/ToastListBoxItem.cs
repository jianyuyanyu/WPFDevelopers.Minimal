using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;

namespace WidgetDesign.Avalonia.Controls
{
    public class ToastListBoxItem : ContentControl
    {
        private const double DismissDelayMs = 10000;
        private CancellationTokenSource? _dismissToken;

        public static readonly StyledProperty<ToastIcon> ToastIconProperty =
            AvaloniaProperty.Register<ToastListBoxItem, ToastIcon>(nameof(ToastIcon));

        public static readonly StyledProperty<bool> IsCenterProperty =
            AvaloniaProperty.Register<ToastListBoxItem, bool>(nameof(IsCenter));

        public ToastIcon ToastIcon
        {
            get => GetValue(ToastIconProperty);
            set => SetValue(ToastIconProperty, value);
        }

        public bool IsCenter
        {
            get => GetValue(IsCenterProperty);
            set => SetValue(IsCenterProperty, value);
        }

        static ToastListBoxItem()
        {
            AffectsArrange<ToastListBoxItem>(ToastIconProperty, IsCenterProperty);

            ToastIconProperty.Changed.AddClassHandler<ToastListBoxItem>((item, e) =>
            {
                var icon = (ToastIcon)e.NewValue!;
                item.PseudoClasses.Set(":info", icon == ToastIcon.Info);
                item.PseudoClasses.Set(":success", icon == ToastIcon.Success);
                item.PseudoClasses.Set(":warning", icon == ToastIcon.Warning);
                item.PseudoClasses.Set(":error", icon == ToastIcon.Error);
            });

            IsCenterProperty.Changed.AddClassHandler<ToastListBoxItem>((item, e) =>
                item.PseudoClasses.Set(":center", (bool)e.NewValue!));
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            var closeBtn = e.NameScope.Find<Button>("PART_CloseButton");
            if (closeBtn != null)
            {
                closeBtn.Click += (_, _) => Close();
            }
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            StartAutoDismissTimer();
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            _dismissToken?.Cancel();
            _dismissToken = null;
            base.OnDetachedFromVisualTree(e);
        }

        private void StartAutoDismissTimer()
        {
            _dismissToken?.Cancel();
            _dismissToken = new CancellationTokenSource();
            var token = _dismissToken.Token;

            Task.Delay(TimeSpan.FromMilliseconds(DismissDelayMs), token).ContinueWith(t =>
            {
                if (t.IsCanceled) return;
                Dispatcher.UIThread.Post(() =>
                {
                    if (IsVisible)
                        RemoveSelf();
                });
            }, CancellationToken.None);
        }

        public void Close()
        {
            _dismissToken?.Cancel();
            _dismissToken = null;
            RemoveSelf();
        }

        private void RemoveSelf()
        {
            (Parent as Panel)?.Children.Remove(this);
        }
    }
}
