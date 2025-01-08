#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT
using Gley.EasyIAP;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Initialize Gley IAP")]
    public class Initialize : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when initialization was successful")]
        public FsmEvent initializationDone;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when initialization failed")]
        public FsmEvent initializationFailed;


        public override void Reset()
        {
            base.Reset();
            initializationDone = null;
            initializationFailed = null;
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (!Gley.EasyIAP.API.IsInitialized())
            {
                Gley.EasyIAP.API.Initialize(InitializationResult);
            }
            else
            {
                Finish();
            }
        }

        private void InitializationResult(IAPOperationStatus status, string message, List<StoreProduct> storeProducts)
        {
            if(status == IAPOperationStatus.Success)
            {
                Fsm.Event(eventTarget, initializationDone);
            }
            else
            {
                Fsm.Event(eventTarget, initializationFailed);
            }
            Finish();
        }
    }
}
#endif
#endif