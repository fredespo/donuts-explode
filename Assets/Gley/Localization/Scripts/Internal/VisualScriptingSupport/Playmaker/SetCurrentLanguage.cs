#if GLEY_PLAYMAKER_SUPPORT

using Gley.Localization;

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/localization/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Get localized string for a given ID")]
	public class SetCurrentLanguage: FsmStateAction
	{
		[Tooltip("ID to translate")]
		public SupportedLanguages language;

		public override void OnEnter()
		{
			Gley.Localization.API.SetCurrentLanguage(language);
			Finish();
		}
	}
}
#endif
