#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-notifications/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Cancel all pending notifications")]
    public class CancelAllNotifications : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.Notifications.API.CancelAllNotifications();
            Finish();
        }
    }
}
#endif
