#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if an app open ad is available")]
    public class IsAppOpenAvailable : FsmStateAction
    {
        [Tooltip("Event triggered if an app open ad is ready to show")]
        public FsmEvent yes;

        [Tooltip("Event triggered if no app open ad is available")]
        public FsmEvent no;

        public override void OnEnter()
        {
            if (Gley.MobileAds.API.IsAppOpenAvailable())
            {
                Fsm.Event(FsmEventTarget.Self, yes);
            }
            else
            {
                Fsm.Event(FsmEventTarget.Self, no);
            }
            Finish();
        }
    }
}
#endif
