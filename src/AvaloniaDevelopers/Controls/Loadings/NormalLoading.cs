using Avalonia.Animation;

namespace AvaloniaDevelopers.Controls
{
    public class NormalLoading : LoadingBase
    {
        static NormalLoading()
        {
            AffectsRender<NormalLoading>(IsLoadingProperty);
        }
    }
}
