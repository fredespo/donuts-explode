#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;
using UnityEngine;

namespace Gley.CrossPromo.Internal
{
    [IncludeInSettings(true)]
    public class CrossPromoUVS
    {
        static GameObject initializeEventTarget;
        static GameObject buttonEventTarget;
        public static void AutoShowPopup(GameObject _eventTarget)
        {
            buttonEventTarget = _eventTarget;
            Gley.CrossPromo.API.AutoShowPopupWhenReady(PopupClosed);
        }

        public static void Initialize(GameObject _eventTarget)
        {
            initializeEventTarget = _eventTarget;
            Gley.CrossPromo.API.Initialize(InitializationComplete);
        }

        public static void ShowPromo(GameObject _eventTarget)
        {
            buttonEventTarget = _eventTarget;
            Gley.CrossPromo.API.ShowCrossPromoPopup(PopupClosed);
        }

        public static void ForceShowPromo(GameObject _eventTarget)
        {
            buttonEventTarget = _eventTarget;
            Gley.CrossPromo.API.ForceShowPopup(PopupClosed);
        }

        private static void InitializationComplete(bool success, string arg1)
        {
            CustomEvent.Trigger(initializeEventTarget, "InitializationComplete", success);
        }

        private static void PopupClosed(bool arg0, string arg1)
        {
            CustomEvent.Trigger(buttonEventTarget, "PopupClosed");
        }
    }
}
#endif
