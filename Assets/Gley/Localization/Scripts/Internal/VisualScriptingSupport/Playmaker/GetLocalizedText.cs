#if GLEY_PLAYMAKER_SUPPORT

using Gley.Localization;

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/localization/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Get localized string for a given ID")]
	public class GetLocalizedText : FsmStateAction
	{
		[Tooltip("ID to translate")]
		public WordIDs textID;

		[UIHint(UIHint.Variable)]
		public FsmString translation;

		public override void Reset()
		{
			translation = null;
		}

		public override void OnEnter()
		{
			translation.Value = Gley.Localization.API.GetText(textID);
			Finish();
		}
	}
}
#endif
