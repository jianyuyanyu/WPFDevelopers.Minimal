using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace WidgetDesign.Avalonia.Controls
{
    public class ToastLayer : Panel
    {
        private Position _position = Position.Top;

        public Position Position
        {
            get => _position;
            set
            {
                _position = value;
                InvalidateArrange();
            }
        }

        public ToastLayer()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
        }

        internal void Push(string message, ToastIcon type, bool center)
        {
            var item = new ToastListBoxItem
            {
                Content = message,
                ToastIcon = type,
                IsCenter = center
            };
            Children.Insert(0, item);
        }

        internal void Clear()
        {
            Children.Clear();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var child in Children)
            {
                child.Measure(availableSize);
            }
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double y = 0;
            var gap = 8.0;

            switch (_position)
            {
                case Position.Top:
                    y = 16;
                    break;
                case Position.Right:
                    y = 16;
                    break;
                case Position.Bottom:
                    y = 16;
                    break;
                case Position.Left:
                    y = 16;
                    break;
            }

            foreach (var child in Children)
            {
                child.Measure(finalSize);
                var desired = child.DesiredSize;

                double childX;
                switch (_position)
                {
                    case Position.Right:
                        childX = finalSize.Width - desired.Width - 16;
                        break;
                    case Position.Left:
                        childX = 16;
                        break;
                    default:
                        childX = (finalSize.Width - desired.Width) / 2;
                        break;
                }

                child.Arrange(new Rect(new Point(childX, y), desired));
                y += desired.Height + gap;
            }
            return finalSize;
        }
    }
}
