#if GLEY_PLAYMAKER_SUPPORT
using Gley.GameServices;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Submit an Achievement")]
    public class SubmitAchievement : FsmStateAction
    {
        [Tooltip("Achievement to submit")]
        public AchievementNames achievement;

        public override void OnEnter()
        {
            Gley.GameServices.API.SubmitAchievement(achievement);
            Finish();
        }
    }
}
#endif
