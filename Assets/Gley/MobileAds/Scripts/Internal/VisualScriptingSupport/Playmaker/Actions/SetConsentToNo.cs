#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Set consent to no (use random ads)")]
    public class SetConsentToNo : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.MobileAds.API.SetGDPRConsent(false);
            Finish();
        }
    }
}
#endif
