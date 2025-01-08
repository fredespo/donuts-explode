#if GLEY_PLAYMAKER_SUPPORT
using Gley.GameServices;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Submit an incremental Achievement")]
    public class IncrementAchievement : FsmStateAction
    {
        [Tooltip("Achievement to submit")]
        public AchievementNames achievement;
        [Tooltip("Units to be incremented")]
        public int steps;

        public override void OnEnter()
        {
            Gley.GameServices.API.IncrementAchievement(achievement, steps);
            Finish();
        }
    }
}
#endif
