using Gley.RateGame.Internal;
using UnityEngine.Events;

namespace Gley.RateGame
{
    public class API
    {
        /// <summary>
        /// When all conditions from Settings Window are met the rate popup is shown. If not this method does nothing.
        /// </summary>
        /// <param name="popupClosed">Callback called when Rate Popup was closed</param>
        public static void ShowRatePopup(UnityAction popupClosed = null)
        {
            RateGameManager.Instance.ShowRatePopup(popupClosed);
        }


        /// <summary>
        /// When all conditions from Settings Window are met the rate popup is shown. If not this method does nothing.
        /// </summary>
        /// <param name="popupClosed">Callback called when Rate Popup was closed with additional parameters</param>
        public static void ShowRatePopupWithCallback(UnityAction<PopupOptions, string> popupClosed = null)
        {
            RateGameManager.Instance.ShowRatePopupWithCallback(popupClosed);
        }


        /// <summary>
        /// Shows the rate popup even if not all conditions from Settings Window are met.
        /// </summary>
        /// <param name="popupClosed">Callback called when Rate Popup was closed</param>
        public static void ForceShowRatePopup(UnityAction popupClosed = null)
        {
            RateGameManager.Instance.ForceShowRatePopup(popupClosed);
        }


        /// <summary>
        /// Shows the rate popup even if not all conditions from Settings Window are met with additional callback parameters.
        /// </summary>
        /// <param name="popupClosed">Callback called when Rate Popup was closed with additional parameters</param>
        public static void ForceShowRatePopupWithCallback(UnityAction<PopupOptions, string> popupClosed = null)
        {
            RateGameManager.Instance.ForceShowRatePopupWithCallback(popupClosed);
        }


        /// <summary>
        /// Called to increase a custom event
        /// </summary>
        public static void IncreaseCustomEvents()
        {
            RateGameManager.Instance.IncreaseCustomEvents();
        }


        /// <summary>
        /// Checks all conditions from Settings Window and determines it they are met or not
        /// </summary>
        /// <returns>true - if all conditions are met</returns>
        public static bool CanShowRate()
        {
            return RateGameManager.Instance.CanShowRate();
        }
    }
}