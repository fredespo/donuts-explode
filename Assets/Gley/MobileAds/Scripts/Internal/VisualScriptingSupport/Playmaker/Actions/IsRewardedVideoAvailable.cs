#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if user consent was already set")]
    public class IsRewardedVideoAvailable : FsmStateAction
    {
        [Tooltip("Event triggered if an rewarded video is ready to show")]
        public FsmEvent yes;

        [Tooltip("Event triggered if no rewarded video is available")]
        public FsmEvent no;

        public override void OnEnter()
        {
            if (Gley.MobileAds.API.IsRewardedVideoAvailable())
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