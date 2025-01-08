#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-notifications/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Initialize Notifications")]
    public class InitializeNotifications : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.Notifications.API.Initialize();
            Finish();
        }
    }
}
#endif
