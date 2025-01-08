#if GLEY_PLAYMAKER_SUPPORT
using Gley.GameServices;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/easy-achievements/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Get player rank from leaderboard")]

    public class GetPlayerRank : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Leaderboard to get the rank from.")]
        public LeaderboardNames leaderboard;

        [Tooltip("Variable where the rank will be stored.")]
        public FsmInt rank;

        public override void Reset()
        {
            base.Reset();
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Gley.GameServices.API.GetPlayerRank(leaderboard, CompleteMethod);
        }

        private void CompleteMethod(long rank)
        {
            this.rank.Value = (int)rank;
        }
    }
}
#endif