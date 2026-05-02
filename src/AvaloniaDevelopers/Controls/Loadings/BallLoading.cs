using Avalonia.Animation;

namespace AvaloniaDevelopers.Controls
{
    public class BallLoading : LoadingBase
    {
        static BallLoading()
        {
            AffectsRender<BallLoading>(IsLoadingProperty);
        }
    }
}
