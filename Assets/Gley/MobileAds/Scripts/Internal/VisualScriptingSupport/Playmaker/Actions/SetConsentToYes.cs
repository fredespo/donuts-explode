#if GLEY_PLAYMAKER_SUPPORT
using Gley.MobileAds.Internal;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Set consent to yes (use personalized ads)")]
    public class SetConsentToYes : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.SetGDPRConsent(true);
            Finish();
        }
    }
}
#endif
