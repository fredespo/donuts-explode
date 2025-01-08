#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-notifications/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Check if notification permission was granted")]
    public class IsPermissionGranted : FsmStateAction
    {
        [Tooltip("Event triggered if app has notification permission")]
        public FsmEvent yes;

        [Tooltip("Event triggered if app does not have notification permission")]
        public FsmEvent no;

        public override void OnEnter()
        {
            if (!Gley.Notifications.API.IsPermissionGranted())
            {
                Fsm.Event(FsmEventTarget.Self, no);
            }
            else
            {
                Fsm.Event(FsmEventTarget.Self, yes);
            }
            Finish();
        }
    }
}
#endif