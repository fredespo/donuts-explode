#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT

using Gley.EasyIAP;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if a Non Consumable or a Subscription was already bought")]
    public class CheckIfBought : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Product to check for")]
        public ShopProductNames productToCheck;

        [Tooltip("Event triggered if the product is already owned")]
        public FsmEvent yes;

        [Tooltip("Event triggered if the product is not owned")]
        public FsmEvent no;

        public override void Reset()
        {
            base.Reset();
            eventTarget = FsmEventTarget.Self;
            yes = null;
            no = null;
        }

        public override void OnEnter()
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                if(Gley.EasyIAP.API.IsActive(productToCheck))
                {
                    Fsm.Event(eventTarget, yes);
                }
                else
                {
                    Fsm.Event(eventTarget, no);
                }
            }
            else
            {
                Fsm.Event(eventTarget, no);
            }
            Finish();
        }
    }
}
#endif
#endif