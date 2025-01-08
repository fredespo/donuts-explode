#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Disable Banner and interstitial ads")]
    public class RemoveAds : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.RemoveAds(true);
            Finish();
        }
    }
}
#endif
