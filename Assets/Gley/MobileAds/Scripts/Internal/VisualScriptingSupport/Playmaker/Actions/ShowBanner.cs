#if GLEY_PLAYMAKER_SUPPORT
using Gley.MobileAds;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays a banner")]
    public class ShowBanner : FsmStateAction
    {
        [Tooltip("Location of the banner")]
        public BannerPosition bannerPosition;
        [Tooltip("Banner Type")]
        public BannerType bannerType;

        public override void OnEnter()
        {
            Gley.MobileAds.API.ShowBanner(bannerPosition, bannerType);
            Finish();
        }
    }
}
#endif
