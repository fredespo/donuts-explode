using UnityEngine;
using UnityEngine.UI;

namespace Gley.MobileAds.Internal
{
    public class MobileAdsTest : MonoBehaviour
    {
        int coins = 0;
        [SerializeField] private Text advertiser;
        [SerializeField] private Text coinsText;
        [SerializeField] private Button intersttialButton;
        [SerializeField] private Button rewardedButton;
        [SerializeField] private Button rewardedInterstitialButton;
        [SerializeField] private Button appOpenButton;
        [SerializeField] private LogWindow logWindow;
        [SerializeField] private Button debugWindowButton;
        [SerializeField] private Button consentWindowButton;

        //buttons
        [SerializeField] private GameObject showMrec;
        [SerializeField] private GameObject hideMrec;
        [SerializeField] private GameObject openDebug;
        [SerializeField] private GameObject openConsent;


        /// <summary>
        /// Initialize the ads
        /// </summary>
        void Awake()
        {
            API.Initialize();
            SupportedAdvertisers selectedAdvertiser = API.GetSelectedAdvertiser().advertiser;
            advertiser.text = selectedAdvertiser.ToString();
            //hide buttons
            switch (selectedAdvertiser)
            {
                case SupportedAdvertisers.Admob:
                    break;
                case SupportedAdvertisers.AppLovin:
                    rewardedInterstitialButton.gameObject.SetActive(false);
                    break;
                case SupportedAdvertisers.LevelPlay:
                    openDebug.SetActive(false);
                    rewardedInterstitialButton.gameObject.SetActive(false);
                    appOpenButton.gameObject.SetActive(false);
                    break;
                case SupportedAdvertisers.UnityLegacy:
                    openDebug.SetActive(false);
                    openConsent.SetActive(false);
                    showMrec.SetActive(false);
                    hideMrec.SetActive(false);
                    rewardedInterstitialButton.gameObject.SetActive(false);
                    appOpenButton.gameObject.SetActive(false);
                    break;
                case SupportedAdvertisers.Vungle:
                    openDebug.SetActive(false);
                    openConsent.SetActive(false);
                    rewardedInterstitialButton.gameObject.SetActive(false);
                    appOpenButton.gameObject.SetActive(false);
                    break;
            }
#if UNITY_IOS
            if (selectedAdvertiser != SupportedAdvertisers.Admob)
            {
                consentWindowButton.gameObject.SetActive(false);
            }
#endif

        }

        void Start()
        {
            coinsText.text = coins.ToString();
        }

        /// <summary>
        /// Show banner assigned from inspector
        /// </summary>
        public void ShawBanner()
        {
            API.ShowBanner(BannerPosition.Top, BannerType.Banner);
        }


        /// <summary>
        /// Hide banner assigned from inspector
        /// </summary>
        public void HideBanner()
        {
            API.HideBanner();
        }


        /// <summary>
        /// Show a medium rectangle ad, assigned from inspector
        /// </summary>
        public void ShowMRec()
        {
            API.ShowMRec(BannerPosition.Top);
        }


        /// <summary>
        /// Hide medium rectangle ad
        /// </summary>
        public void HideMRec()
        {
            API.HideMRec();
        }

        /// <summary>
        /// Show Interstitial assigned from inspector
        /// </summary>
        public void ShowInterstitial()
        {
            API.ShowInterstitial();
        }

        /// <summary>
        /// Show Open App ad assigned from inspector
        /// </summary>
        public void ShowAppOpen()
        {
            API.ShowAppOpen();
        }

        /// <summary>
        /// Show rewarded video assigned from inspector
        /// </summary>
        public void ShowRewardedVideo()
        {
            API.ShowRewardedVideo(CompleteMethod);
        }

        /// <summary>
        /// Show rewarded interstitial assigned from inspector
        /// </summary>
        public void ShowRewardedInterstitial()
        {
            API.ShowRewardedInterstitial(CompleteMethod);
        }

        /// <summary>
        /// Callback called when a rewarded video or interstitial is complete
        /// </summary>
        /// <param name="completed"></param>
        private void CompleteMethod(bool completed)
        {
            if (completed)
            {
                coins += 100;
            }
            coinsText.text = coins.ToString();
            GleyLogger.AddLog($"Completed: {completed}");
        }

        /// <summary>
        /// Open debug window
        /// </summary>
        public void OpenDebugWindow()
        {
            API.OpenDebugWindow();
        }

        /// <summary>
        /// Open consent popup
        /// </summary>
        public void OpenConsentWindow()
        {
            API.ShowBuiltInConsentPopup(ConsentPopupClosed);
        }


        /// <summary>
        /// Callback called when consent popup is closed
        /// </summary>
        private void ConsentPopupClosed()
        {
            GleyLogger.AddLog($"Consent Popup Closed");
        }

        /// <summary>
        /// This is for testing purpose
        /// </summary>
        void Update()
        {
            if (API.IsInterstitialAvailable())
            {
                intersttialButton.interactable = true;
            }
            else
            {
                intersttialButton.interactable = false;
            }

            if (API.IsRewardedVideoAvailable())
            {
                rewardedButton.interactable = true;
            }
            else
            {
                rewardedButton.interactable = false;
            }

            if (API.IsRewardedInterstitialAvailable())
            {
                rewardedInterstitialButton.interactable = true;
            }
            else
            {
                rewardedInterstitialButton.interactable = false;
            }

            if (API.IsAppOpenAvailable())
            {
                appOpenButton.interactable = true;
            }
            else
            {
                appOpenButton.interactable = false;
            }
        }


        /// <summary>
        /// View debug messages
        /// </summary>
        public void ShowLogsWindow()
        {
            logWindow.ShowLogWindow();
        }

        
        // AppOpen example.
        private void OnApplicationPause(bool pause)
        {
            if (pause == false)
            {
                Gley.MobileAds.API.ShowAppOpen();
            }
        }
    }
}
