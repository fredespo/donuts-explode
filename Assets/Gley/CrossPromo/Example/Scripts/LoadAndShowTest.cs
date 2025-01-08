using UnityEngine;

namespace Gley.CrossPromo.Internal
{
    public class LoadAndShowTest : MonoBehaviour
    {
        private void Start()
        {
            //call to load the Cross Popup Images
            Gley.CrossPromo.API.Initialize(InitializationComplete);
        }


        /// <summary>
        /// Triggered when all images are loaded
        /// </summary>
        /// <param name="error">error message</param>
        private void InitializationComplete(bool success, string error)
        {
            if (success)
            {
                Debug.Log("Load Success");
            }
            else
            {
                Debug.Log(error);
            }
        }


        /// <summary>
        /// Public method used for button
        /// </summary>
        public void ShowPromo()
        {
            //Show Cross Promo Popup
            Gley.CrossPromo.API.ShowCrossPromoPopup(PopupClosed);
        }


        /// <summary>
        /// Public method used for button
        /// </summary>
        public void ForceShowPromo()
        {
            //Show Cross Promo Popup bypassing the settings from Settings Window
            Gley.CrossPromo.API.ForceShowPopup(PopupClosed);
        }


        /// <summary>
        /// Triggered when popup is closed
        /// </summary>
        /// <param name="imageClicked">true if popup was clicked, false if popup was closed by pressing X</param>
        /// <param name="imageName">the name of the clicked image, good for analytics</param>
        private void PopupClosed(bool imageClicked, string imageName)
        {
            Debug.Log("Popup closed");
            if (imageClicked)
            {
                Debug.Log("Image name " + imageName + " was clicked");
            }
        }
    }
}
