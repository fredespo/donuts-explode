using UnityEngine;
using UnityEngine.UI;

namespace Gley.RateGame.Internal
{
    public class YesNoPopup : MonoBehaviour
    {
        public Text mainText;
        public Text yesButtonText;
        public Text noButtonText;
        public Text laterButtonText;
        private PopupOptions result;

        /// <summary>
        /// Set popup texts from Settings Window
        /// </summary>
        private void Start()
        {
            mainText.text = RateGameManager.Instance.RateGameSettings.mainText;
            if (!string.IsNullOrEmpty(RateGameManager.Instance.RateGameSettings.yesButton))
            {
                yesButtonText.text = RateGameManager.Instance.RateGameSettings.yesButton;
            }
            else
            {
                yesButtonText.transform.parent.gameObject.SetActive(false);
            }
            if (!string.IsNullOrEmpty(RateGameManager.Instance.RateGameSettings.noButton))
            {
                noButtonText.text = RateGameManager.Instance.RateGameSettings.noButton;
            }
            else
            {
                noButtonText.transform.parent.gameObject.SetActive(false);
            }
            if (!string.IsNullOrEmpty(RateGameManager.Instance.RateGameSettings.laterButton))
            {
                laterButtonText.text = RateGameManager.Instance.RateGameSettings.laterButton;
            }
            else
            {
                laterButtonText.transform.parent.gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// Button event called from Yes Button 
        /// </summary>
        public void YesButtonClick()
        {
            result = PopupOptions.Rated;
            ClosePopup();
            RateGameManager.Instance.NeverShowPopup();
            RateGameManager.Instance.OpenUrl();
        }


        /// <summary>
        /// Button event called from No button - Never shows the popup
        /// </summary>
        public void NoButtonClick()
        {
            result = PopupOptions.Never;
            ClosePopup();
            RateGameManager.Instance.NeverShowPopup();
        }


        /// <summary>
        /// Button event called from Later button
        /// </summary>
        public void LaterButtonClick()
        {
            result = PopupOptions.NotNow;
            ClosePopup();
        }


        /// <summary>
        /// Make close animation then destroy the popup
        /// </summary>
        private void ClosePopup()
        {
            GetComponent<Animator>().SetTrigger("Close");
            AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            Destroy(gameObject.transform.parent.gameObject, info.length + 0.1f);
            Invoke("CloseEvent", info.length);
        }


        /// <summary>
        /// Trigger close popup event
        /// </summary>
        private void CloseEvent()
        {
            RateGameManager.Instance.RatePopupWasClosed(result, null);
        }
    }
}
