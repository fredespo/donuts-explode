#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT

using Gley.EasyIAP;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Get product value from settings window")]
    public class GetProductValue : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Product to get the price for")]
        public ShopProductNames productToCheck;

        [Tooltip("Variable where the product value will be stored")]
        public FsmInt value;


        public override void Reset()
        {
            base.Reset();
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                value.Value = Gley.EasyIAP.API.GetValue(productToCheck);
            }
            else
            {
                value.Value = 0;
            }
            Finish();
        }
    }
}
#endif
#endif
