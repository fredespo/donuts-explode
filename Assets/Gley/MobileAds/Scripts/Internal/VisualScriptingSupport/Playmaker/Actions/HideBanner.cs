#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays a banner")]
    public class HideBanner : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.HideBanner();
            Finish();
        }
    }
}
#endif
