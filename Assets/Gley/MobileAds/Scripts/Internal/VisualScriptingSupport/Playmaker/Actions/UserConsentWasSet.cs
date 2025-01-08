#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if user consent was already set")]
    public class UserConsentWasSet : FsmStateAction
    {
        [Tooltip("Event triggered if the user consent was already set")]
        public FsmEvent yes;

        [Tooltip("Event triggered if the user consent was not set")]
        public FsmEvent no;

        public override void OnEnter()
        {
            if (Gley.MobileAds.API.GDPRConsentWasSet())
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
