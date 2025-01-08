#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Show a built in consent popup. if advertiser has one, it will be shown, otherwise the default popup available in the package will be shown")]
    public class ShowBuiltInConsentPopup : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when the consent popup is closed")]
        public FsmEvent consentPopupClosed;


        public override void Reset()
        {
            base.Reset();
            consentPopupClosed = null;
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            Gley.MobileAds.API.ShowBuiltInConsentPopup(PopupClosed);
        }

        private void PopupClosed()
        {
            Fsm.Event(eventTarget, consentPopupClosed);
            Finish();
        }
    }
}
#endif
