#if GLEY_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/localization/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Get localized string for a given ID")]
	public class NextLanguage : FsmStateAction
	{
		public override void OnEnter()
		{
            Gley.Localization.API.NextLanguage();
            Gley.Localization.API.SetCurrentLanguage(Gley.Localization.API.GetCurrentLanguage());
			Finish();
		}
	}
}
#endif
