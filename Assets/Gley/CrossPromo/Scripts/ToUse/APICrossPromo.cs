using UnityEngine.Events;
using Gley.CrossPromo.Internal;

namespace Gley.CrossPromo
{
    public class API
    {
        /// <summary>
        /// Starts loading the settings file and cross promo images
        /// </summary>
        /// <param name="completeMethod">Callback triggered when all is loaded</param>
        public static void Initialize(UnityAction<bool, string> completeMethod = null)
        {
            CrossPromoManager.Instance.Initialize(completeMethod);
        }


        /// <summary>
        /// Display the Cross Promo Popup if all conditions from Settings Window are met
        /// </summary>
        /// <param name="popupClosed">Callback triggered when Cross Promo Popup is closed</param>
        public static void ShowCrossPromoPopup(UnityAction<bool, string> popupClosed = null)
        {
            CrossPromoManager.Instance.ShowCrossPromoPopup(popupClosed);
        }


        /// <summary>
        /// Display the Cross Promo Popup if is loaded, no conditions are checked
        /// </summary>
        /// <param name="popupClosed">Callback triggered when Cross Promo Popup is closed</param>
        public static void ForceShowPopup(UnityAction<bool, string> popupClosed = null)
        {
            CrossPromoManager.Instance.ForceShowPopup(popupClosed);
        }


        /// <summary>
        /// Start loading the images and when are ready display the Cross Promo Popup is conditions from Settings Window are met
        /// </summary>
        /// <param name="popupClosed">Callback triggered when Cross Promo Popup is closed</param>
        public static void AutoShowPopupWhenReady(UnityAction<bool, string> popupClosed = null)
        {
            CrossPromoManager.Instance.AutoShowPopupWhenReady(popupClosed);
        }
    }
}