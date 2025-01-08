#if GLEY_PLAYMAKER_SUPPORT
using Gley.GameServices;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Submit score into a leaderboard")]
    public class SubmitScore : FsmStateAction
    {
        [Tooltip("Leaderboard to submit your score")]
        public LeaderboardNames leaderboard;

        [UIHint(UIHint.Variable)]
        [Tooltip("Score to sumbit")]
        public FsmInt score;

        public override void OnEnter()
        {
            Gley.GameServices.API.SubmitScore(score.Value, leaderboard);
            Finish();
        }
    }
}
#endif
