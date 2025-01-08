#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays an app open ad")]
    public class ShowAppOpen : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.ShowAppOpen();
            Finish();
        }
    }
}
#endif
