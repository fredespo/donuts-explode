using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gley.RateGame.Internal
{
    public class StarsPopup : MonoBehaviour
    {
        public Text mainText;
        public Text sendButton;
        public Text notNowButton;
        public Text neverButton;
        public Transform starsHolder;
        public Button send;

        private bool openUrl;
        private PopupOptions result;

        /// <summary>
        /// Set popup texts from Settings Window
        /// </summary>
        private void Start()
        {
            mainText.text = RateGameManager.Instance.RateGameSettings.mainText;
            if (!string.IsNullOrEmpty(RateGameManager.Instance.RateGameSettings.sendButton))
            {
                sendButton.text = RateGameManager.Instance.RateGameSettings.sendButton;
            }
            else
            {
                sendButton.transform.parent.gameObject.SetActive(false);
            }
            if (!string.IsNullOrEmpty(RateGameManager.Instance.RateGameSettings.notNowButton))
            {
                notNowButton.text = RateGameManager.Instance.RateGameSettings.notNowButton;
            }
            else
            {
                notNowButton.transform.parent.gameObject.SetActive(false);
            }

            if (!string.IsNullOrEmpty(RateGameManager.Instance.RateGameSettings.neverButton))
            {
                neverButton.text = RateGameManager.Instance.RateGameSettings.neverButton;
            }
            else
            {
                neverButton.transform.parent.gameObject.SetActive(false);
            }
            send.interactable = false;
            for (int i = 0; i < starsHolder.childCount; i++)
            {
                starsHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Button event called from Send Button 
        /// </summary>
        public void SendButtonClick()
        {
            result = PopupOptions.Rated;
            ClosePopup();
            RateGameManager.Instance.NeverShowPopup();
            if (openUrl)
            {
                RateGameManager.Instance.OpenUrl();
            }
        }


        /// <summary>
        /// Button event called from Later button
        /// </summary>
        public void NotNowButton()
        {
            result = PopupOptions.NotNow;
            ClosePopup();
        }


        /// <summary>
        /// Button event called from never button
        /// </summary>
        public void NeverButton()
        {
            result = PopupOptions.Never;
            ClosePopup();
            RateGameManager.Instance.NeverShowPopup();
        }


        /// <summary>
        /// Called when a star is clicked, activates the required stars
        /// </summary>
        /// <param name="star"></param>
        public void StarClicked(GameObject star)
        {
            int starNUmber = int.Parse(star.name.Split('_')[1]);
            if (starNUmber + 1 < RateGameManager.Instance.RateGameSettings.minStarsToSend)
            {
                openUrl = false;
            }
            else
            {
                openUrl = true;
            }
            for (int i = 0; i < starsHolder.childCount; i++)
            {
                if (i <= starNUmber)
                {
                    starsHolder.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    starsHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
            }
            send.interactable = true;
        }


        /// <summary>
        /// Make close animation then destroy the popup
        /// </summary>
        private void ClosePopup()
        {
            GetComponent<Animator>().SetTrigger("Close");
            AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            Destroy(gameObject.transform.parent.gameObject, info.length + 0.1f);
            StartCoroutine(CloseEvent(info.length));
        }


        /// <summary>
        /// Trigger close popup event
        /// </summary>
        private IEnumerator CloseEvent(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            RateGameManager.Instance.RatePopupWasClosed(result, null);
        }
    }
}
