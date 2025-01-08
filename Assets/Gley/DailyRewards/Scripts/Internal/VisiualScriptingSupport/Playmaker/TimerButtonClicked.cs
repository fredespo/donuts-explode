#if GLEY_PLAYMAKER_SUPPORT

using Gley.DailyRewards;

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/daily-rewards/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Show Calendar Popup")]
	public class TimerButtonClicked : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Event sent when a calendar button is clicked and time expired")]
		public FsmEvent buttonClicked;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Event sent when a calendar button is clicked but timer is still active")]
		public FsmEvent timerActive;

		public FsmEnum clickedButtonID;

		public override void Reset()
		{
			base.Reset();
			eventTarget = FsmEventTarget.Self;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Gley.DailyRewards.API.TimerButton.AddClickListener(RewardButtonClicked);
		}

		private void RewardButtonClicked(TimerButtonIDs buttonID, bool timeExpired)
		{
			clickedButtonID.Value = buttonID;
			if(timeExpired)
			{
				Fsm.Event(eventTarget, buttonClicked);
			}
			else
			{
				Fsm.Event(eventTarget, timerActive);
			}
		}
	}
}
#endif
