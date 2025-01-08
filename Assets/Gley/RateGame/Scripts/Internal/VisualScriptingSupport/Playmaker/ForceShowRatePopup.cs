#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/rate-game/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Show Rate Popup bypassing the settings form Settings Window")]
    public class ForceShowRatePopup : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.RateGame.API.ForceShowRatePopup();
            Finish();
        }
    }
}
#endif
