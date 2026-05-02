using Avalonia.Animation;

namespace AvaloniaDevelopers.Controls
{
    public class RingLoading : LoadingBase
    {
        static RingLoading()
        {
            AffectsRender<RingLoading>(IsLoadingProperty);
        }
    }
}
