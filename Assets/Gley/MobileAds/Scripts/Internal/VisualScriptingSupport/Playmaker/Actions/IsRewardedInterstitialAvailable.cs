#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if a rewarded interstitial is available")]
    public class IsRewardedInterstitialAvailable : FsmStateAction
    {
        [Tooltip("Event triggered if an rewarded interstitial is ready to show")]
        public FsmEvent yes;

        [Tooltip("Event triggered if no rewarded interstitial is available")]
        public FsmEvent no;

        public override void OnEnter()
        {
            if (Gley.MobileAds.API.IsRewardedInterstitialAvailable())
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