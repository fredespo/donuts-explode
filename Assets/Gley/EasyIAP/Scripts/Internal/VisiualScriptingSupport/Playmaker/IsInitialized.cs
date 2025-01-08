#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-iap/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if IAP is initialized")]
    public class IsInitialized : FsmStateAction
    {

        [Tooltip("Event triggered if IAP is initializes")]
        public FsmEvent yes;

        [Tooltip("Event triggered if IAP is not initialized")]
        public FsmEvent no;

        public override void OnEnter()
        {
            if (Gley.EasyIAP.API.IsInitialized())
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
#endif
