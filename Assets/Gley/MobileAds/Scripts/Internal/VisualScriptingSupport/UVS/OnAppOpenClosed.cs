#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;

namespace Gley.MobileAds.Internal
{
    [UnitCategory("Events\\Gley\\MobileAds")]
    public class OnAppOpenClosed : EventUnit<int>
    {
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OnAppOpenClosed);
        }
    }
}
#endif