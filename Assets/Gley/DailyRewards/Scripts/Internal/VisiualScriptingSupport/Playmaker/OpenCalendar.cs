#if GLEY_PLAYMAKER_SUPPORT
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/daily-rewards/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Show Calendar Popup")]
	public class OpenCalendar : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Event sent when a calendar button is clicked")]
		public FsmEvent buttonClicked;
		public FsmInt dayNumber;
		public FsmInt rewardValue;
		public FsmObject rewardSprite;

		public override void Reset()
		{
			base.Reset();
			eventTarget = FsmEventTarget.Self;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Gley.DailyRewards.API.Calendar.Show();
			Gley.DailyRewards.API.Calendar.AddClickListener(CalendarButtonClicked);
		}

		private void CalendarButtonClicked(int dayNumber, int rewardValue, Sprite rewardSprite)
		{
			this.dayNumber.Value = dayNumber;
			this.rewardValue.Value = rewardValue;
			this.rewardSprite.Value = rewardSprite;
			Fsm.Event(eventTarget, buttonClicked);
		}
	}
}
#endif
