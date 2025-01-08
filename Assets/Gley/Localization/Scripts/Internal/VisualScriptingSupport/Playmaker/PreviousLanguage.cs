#if GLEY_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/localization/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Get localized string for a given ID")]
	public class PreviousLanguage : FsmStateAction
	{
		public override void OnEnter()
		{
            Gley.Localization.API.PreviousLanguage();
            Gley.Localization.API.SetCurrentLanguage(Gley.Localization.API.GetCurrentLanguage());
			Finish();
		}
	}
}
#endif