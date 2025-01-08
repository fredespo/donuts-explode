#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/rate-game/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Increase events for rate popup display")]
    public class IncreaseCustomEvents : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.RateGame.API.IncreaseCustomEvents();
            Finish();
        }
    }
}
#endif
