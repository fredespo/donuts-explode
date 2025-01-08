using Gley.DailyRewards.Internal;
#if GLEY_LOCALIZATION
using Gley.Localization.Internal;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Gley.DailyRewards
{
#if GLEY_LOCALIZATION
    public class TimerButtonScript : MonoBehaviour, ILocalizationComponent
#else
    public class TimerButtonScript : MonoBehaviour
#endif
    {
        public TimerButtonIDs buttonID;
        public Button buttonScript;
        public Text buttonText;

        private string completeText;
        private float currentTime;
        private bool initialized;

        const float refreshTime = 0.3f;

        void Start()
        {
            //Initialize the current button
            TimerButtonManager.Instance.Initialize(buttonID, InitializationComplete);
        }


        /// <summary>
        /// Setup the button
        /// </summary>
        /// <param name="remainingTime">time until ready</param>
        /// <param name="interactable">is button clickable</param>
        /// <param name="completeText">the text that appears after timer is done</param>
        private void InitializationComplete(string remainingTime, bool interactable, string completeText)
        {
            this.completeText = completeText;

            buttonText.text = remainingTime;
            buttonScript.interactable = interactable;
            RefreshButton();
        }


        /// <summary>
        /// refresh button text
        /// </summary>
        void Update()
        {
            if (initialized)
            {
                currentTime += Time.deltaTime;
                if (currentTime > refreshTime)
                {
                    currentTime = 0;
                    RefreshButton();
                }
            }
        }

        public void Refresh()
        {
#if GLEY_LOCALIZATION
            TimerButtonLocalization localization = gameObject.GetComponent<TimerButtonLocalization>();
            if (localization != null)
            {
                buttonText.text = completeText = localization.GetText();
            }
#endif
        }


        /// <summary>
        /// update button appearance
        /// </summary>
        private void RefreshButton()
        {
            buttonText.text = TimerButtonManager.Instance.GetRemainingTime(buttonID);
            if (TimerButtonManager.Instance.TimeExpired(buttonID))
            {
                Refresh();
                buttonText.text = completeText;
                buttonScript.interactable = true;
                initialized = false;
            }
            else
            {
                initialized = true;
            }
        }


        /// <summary>
        /// Listener triggered when button is clicked
        /// </summary>
        public void RewardButtonClicked()
        {
            TimerButtonManager.Instance.ButtonClicked(buttonID, ClickResult);
        }


        /// <summary>
        /// Reset the button state if clicked and the reward was collected
        /// </summary>
        /// <param name="timeExpired"></param>
        private void ClickResult(bool timeExpired)
        {
            if (timeExpired)
            {
                TimerButtonManager.Instance.Initialize(buttonID, InitializationComplete);
            }
        }
    }
}
