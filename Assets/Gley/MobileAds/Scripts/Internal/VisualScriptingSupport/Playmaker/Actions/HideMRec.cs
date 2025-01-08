#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Hides a MRec")]
    public class HideMRec : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.HideMRec();
            Finish();
        }
    }
}
#endif
