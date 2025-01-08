#if GLEY_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-cross-promo/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Initialize Cross Promo")]
    public class InitializeCrossPromo : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when initialization was successful")]
        public FsmEvent initializationDone;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when initialization failed")]
        public FsmEvent initializationFailed;


        public override void Reset()
        {
            base.Reset();
            initializationDone = null;
            initializationFailed = null;
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            Gley.CrossPromo.API.Initialize(InitializationComplete);
        }

        private void InitializationComplete(bool success, string arg1)
        {
            if(success)
            {
                Fsm.Event(eventTarget, initializationDone);
            }
            else
            {
                Fsm.Event(eventTarget, initializationFailed);
            }
            Finish();
        }
    }
}
#endif
