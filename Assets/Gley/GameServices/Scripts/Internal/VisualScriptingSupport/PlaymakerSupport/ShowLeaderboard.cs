#if GLEY_PLAYMAKER_SUPPORT
using Gley.GameServices;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Show a specific Leaderboard")]
    public class ShowLeaderboard : FsmStateAction
    {
        [Tooltip("Show a leaderboard")]
        public LeaderboardNames leaderboard;

        public override void OnEnter()
        {
            Gley.GameServices.API.ShowSpecificLeaderboard(leaderboard);
            Finish();
        }
    }
}
#endif
