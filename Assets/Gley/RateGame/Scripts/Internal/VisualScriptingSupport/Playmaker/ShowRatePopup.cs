#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/rate-game/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Show Rate Popup")]
    public class ShowRatePopup : FsmStateAction
    {
        public override void OnEnter()
        {
            Gley.RateGame.API.ShowRatePopup();
            Finish();
        }
    }
}
#endif
