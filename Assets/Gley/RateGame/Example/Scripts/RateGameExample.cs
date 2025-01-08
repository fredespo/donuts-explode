using UnityEngine;

namespace Gley.RateGame.Internal
{
    public class RateGameExample : MonoBehaviour
    {
        /// <summary>
        /// Show Rate Game Popup every time this script starts and conditions are met
        /// </summary>
        private void Start()
        {
            Gley.RateGame.API.ShowRatePopupWithCallback(PopupClosedMethod);
        }

        /// <summary>
        /// Increase custom event by pressing UI Button
        /// </summary>
        public void IncreaseCustomEvents()
        {
            Gley.RateGame.API.IncreaseCustomEvents();
        }


        /// <summary>
        /// Show Rate Game Popup even if conditions are not met by pressing the UI Button
        /// </summary>
        public void ForceShowPopup()
        {
            Gley.RateGame.API.ForceShowRatePopupWithCallback(PopupClosedMethod);
            //Gley.RateGame.API.ShowNativeRatePopup();
        }


        /// <summary>
        /// Triggered when Rate Popup is closed
        /// </summary>
        private void PopupClosedMethod(Gley.RateGame.PopupOptions result, string message)
        {
            Debug.Log($"Popup Closed-> Result: {result}, Message: {message} -> Resume Game");
        }
    }
}