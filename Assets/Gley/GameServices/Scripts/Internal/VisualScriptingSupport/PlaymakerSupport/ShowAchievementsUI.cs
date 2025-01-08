#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Show the built in achievements UI")]
    public class ShowAchievementsUI : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.GameServices.API.ShowAchievementsUI();
            Finish();
        }
    }
}
#endif
