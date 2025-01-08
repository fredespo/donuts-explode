#if GLEY_PLAYMAKER_SUPPORT

using Gley.DailyRewards;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/daily-rewards/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Get button timer")]

    public class GetRemainingTime : FsmStateAction
    {
        public FsmEnum buttonToCheck;

        public FsmString fsmTime;

        public override void OnEnter()
        {
            base.OnEnter();
            fsmTime.Value = Gley.DailyRewards.API.TimerButton.GetRemainingTime((TimerButtonIDs)buttonToCheck.Value).ToString();
            Finish();
        }
    }
}
#endif
