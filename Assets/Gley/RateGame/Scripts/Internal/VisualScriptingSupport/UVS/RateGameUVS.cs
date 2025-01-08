#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;

namespace Gley.RateGame.Internal
{
    [IncludeInSettings(true)]
    public static class RateGameUVS
    {
        public static void ShowRatePopup()
        {
            Gley.RateGame.API.ShowRatePopup();
        }

        public static void ForceShowRatePopup()
        {
            Gley.RateGame.API.ForceShowRatePopup();
        }

        public static void IncreaseCustomEvents()
        {
            Gley.RateGame.API.IncreaseCustomEvents();
        }
    }
}
#endif