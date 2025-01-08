#if GLEY_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("https://gley.gitbook.io/localization/")]
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Get the current selected language")]
	public class GetCurrentLanguage : FsmStateAction
	{
		public FsmString currentLanguage;

		public override void Reset()
		{
			currentLanguage = "";
		}

		public override void OnEnter()
		{ 
			currentLanguage.Value = Gley.Localization.API.GetCurrentLanguage().ToString();
			Finish();
		}
	}
}
#endif
