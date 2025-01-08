#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT

using Gley.EasyIAP;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Restores purchases(needed only on iOS)")]
    public class RestorePurchases : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when restore purchases was successful")]
        public FsmEvent restoreDone;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when restore purchases failed")]
        public FsmEvent restoreFailed;


        public override void Reset()
        {
            base.Reset();
            restoreDone = null;
            restoreFailed = null;
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                Gley.EasyIAP.API.RestorePurchases(RestoreResult);
            }
            else
            {
                Fsm.Event(eventTarget, restoreFailed);
                Finish();
            }
        }

        private void RestoreResult(IAPOperationStatus status, string message, StoreProduct product)
        {
            if(status == IAPOperationStatus.Success)
            {
                Fsm.Event(eventTarget, restoreDone);
            }
            else
            {
                Fsm.Event(eventTarget, restoreFailed);
            }
        }
    }
}
#endif
#endif