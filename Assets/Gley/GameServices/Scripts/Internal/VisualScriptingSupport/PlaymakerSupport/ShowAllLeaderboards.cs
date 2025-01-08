#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Show all Leaderboards in the game")]
    public class ShowAllLeaderboards : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.GameServices.API.ShowLeaderboadsUI();
            Finish();
        }
    }
}
#endif
