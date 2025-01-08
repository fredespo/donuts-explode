#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays an interstitial")]
    public class ShowInterstitial : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.ShowInterstitial();
            Finish();
        }
    }
}
#endif
