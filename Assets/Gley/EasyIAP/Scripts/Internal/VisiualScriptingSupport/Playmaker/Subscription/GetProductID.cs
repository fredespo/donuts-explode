#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT

using Gley.EasyIAP;
using UnityEngine.Purchasing;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Get Subscription Product ID")]
    public class GetProductID : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Subscription product")]
        public ShopProductNames subscriptionProductToCheck;

        [Tooltip("Variable where the Product ID will be stored")]
        public FsmString result;


        public override void Reset()
        {
            base.Reset();
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                SubscriptionInfo info = Gley.EasyIAP.API.GetSubscriptionInfo(subscriptionProductToCheck);
                if (info != null)
                {
                    result.Value= info.getProductId();
                }
                else
                {
                    result.Value = "-";
                }

            }
            else
            {
                result.Value = "-";
            }
            Finish();
        }
    }
}
#endif
#endif