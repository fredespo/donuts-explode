#if GLEY_PLAYMAKER_SUPPORT
using Gley.MobileAds;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays a banner")]
    public class ShowMRec : FsmStateAction
    {
        [Tooltip("Location of the banner")]
        public BannerPosition bannerPosition;
        public override void OnEnter()
        {
            Gley.MobileAds.API.ShowBanner(bannerPosition, BannerType.MediumRectangle);
            Finish();
        }
    }
}
#endif
