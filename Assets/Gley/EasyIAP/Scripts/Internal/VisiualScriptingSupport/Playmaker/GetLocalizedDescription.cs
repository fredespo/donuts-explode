#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT

using Gley.EasyIAP;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Get localized description")]
    public class GetLocalizedDescription : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Product to get the description for")]
        public ShopProductNames productToCheck;

        [Tooltip("Variable where the product description will be stored")]
        public FsmString description;


        public override void Reset()
        {
            base.Reset();
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                description.Value = Gley.EasyIAP.API.GetLocalizedDescription(productToCheck);
            }
            else
            {
                description.Value = "-";
            }
            Finish();
        }
    }
}
#endif
#endif