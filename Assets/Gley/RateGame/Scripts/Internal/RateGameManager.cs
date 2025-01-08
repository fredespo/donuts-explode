#if GLEY_NATIVE_GOOGLEPLAY
using Google.Play.Review;
using System.Collections;
#endif

using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gley.RateGame.Internal
{
    public class RateGameManager : MonoBehaviour
    {
        private UnityAction PopupClosed;
        private UnityAction<PopupOptions, string> PopupClosedWithParams;


        //create a static instance
        private static RateGameManager instance;
        internal static RateGameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    //create game object and automatically add the script on it
                    GameObject go = new GameObject("RateGame");
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<RateGameManager>();
                    //every time this objects is created it means that a new session has begun
                    SaveValues.IncreaseNumberOfSessions();
                    //save the open time of the app
                    SaveValues.SetOpenTime(false);
                }
                return instance;
            }
        }


        /// <summary>
        /// Load rate popup settings
        /// </summary>
        private RateGameData rateGameSettings;
        internal RateGameData RateGameSettings
        {
            get
            {
                if (rateGameSettings == null)
                {
                    rateGameSettings = Resources.Load<RateGameData>(Constants.DATA_NAME_RUNTIME);
                    if (rateGameSettings == null)
                    {
                        Debug.LogWarning("Rate Popup is not setup. Go to Tools->Gley->Rate Game to setup");
                        return rateGameSettings;
                    }
#if UNITY_EDITOR
                    if (rateGameSettings.clearSave)
                    {
                        SaveValues.DeleteAll();
                    }
#endif
                }
                return rateGameSettings;
            }
        }


        /// <summary>
        /// When all conditions from Settings Window are met the rate popup is shown. If not this method does nothing.
        /// </summary>
        /// <param name="PopupClosed">callback called when Rate Game Popup was closed</param>
        internal void ShowRatePopup(UnityAction PopupClosed = null)
        {
            //Debug.Log("ShowRatePopup");
            //Debug.Log(RateGameSettings);
            if (RateGameSettings == null)
            {
                return;
            }
            this.PopupClosed = PopupClosed;
            if (CanShowRate())
            {
                DisplayPopup();
            }
        }


        /// <summary>
        /// When all conditions from Settings Window are met the rate popup is shown. If not this method does nothing.
        /// </summary>
        /// <param name="PopupClosed">callback called when Rate Game Popup was closed</param>
        internal void ShowRatePopupWithCallback(UnityAction<PopupOptions, string> PopupClosed = null)
        {
            //Debug.Log("ShowRatePopup");
            //Debug.Log(RateGameSettings);
            if (RateGameSettings == null)
            {
                return;
            }
            PopupClosedWithParams = PopupClosed;
            if (CanShowRate())
            {
                DisplayPopup();
            }
        }


        /// <summary>
        /// Shows the rate popup even if not all conditions from Settings Window ware met
        /// </summary>
        /// <param name="PopupClosed">callback called when Rate Game Popup was closed</param>
        internal void ForceShowRatePopup(UnityAction PopupClosed = null)
        {
            if (RateGameSettings == null)
            {
                return;
            }
            this.PopupClosed = PopupClosed;
            DisplayPopup();
        }


        /// <summary>
        /// Shows the rate popup even if not all conditions from Settings Window ware met
        /// </summary>
        /// <param name="PopupClosed">callback called when Rate Game Popup was closed</param>
        internal void ForceShowRatePopupWithCallback(UnityAction<PopupOptions, string> PopupClosed = null)
        {
            if (RateGameSettings == null)
            {
                return;
            }
            PopupClosedWithParams = PopupClosed;
            DisplayPopup();
        }


        /// <summary>
        /// Constructs and open the rate URL
        /// </summary>
        internal void OpenUrl()
        {
#if UNITY_ANDROID

#if UNITY_EDITOR
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + RateGameSettings.googlePlayBundleID);
#else
        Application.OpenURL("market://details?id=" + RateGameSettings.googlePlayBundleID);
#endif
#endif
#if UNITY_IOS
            Application.OpenURL("https://itunes.apple.com/app/id"+RateGameSettings.iosAppID);
#endif
        }


        /// <summary>
        /// Called to stop showing Rate Popup
        /// </summary>
        internal void NeverShowPopup()
        {
            SaveValues.NeverShowPopup();
        }


        /// <summary>
        /// Called to increase a custom event
        /// </summary>
        internal void IncreaseCustomEvents()
        {
            SaveValues.IncreaseNumberOfCustomEvents();
        }


        /// <summary>
        /// Called by the popup when closes and if there is an active callback it will be triggered
        /// </summary>
        internal void RatePopupWasClosed(PopupOptions result, string message)
        {
            if (PopupClosed != null)
            {
                PopupClosed();
                PopupClosed = null;
            }

            if (PopupClosedWithParams != null)
            {
                PopupClosedWithParams(result, message);
                PopupClosedWithParams = null;
            }
        }


        /// <summary>
        /// Really loads the popup
        /// </summary>
        private void DisplayPopup()
        {
            SaveValues.MarkAsSeen();
#if UNITY_IOS
            if (RateGameSettings.useNativePopupiOS)
#else
            if(RateGameSettings.useNativePopup)
#endif
            {
                ShowNativeRatePopup();
            }
            else
            {
                LoadPopup(RateGameSettings.ratePopupCanvas, RateGameSettings.popupGameObject);
            }
        }


        /// <summary>
        /// Checks all conditions from Settings Window and determines it they are met or not
        /// </summary>
        /// <returns></returns>
        internal bool CanShowRate()
        {
            bool sessionCountReached;
            bool customEventsReached;
            bool timeSinceStartReached;
            bool timeSinceOpenReached;
            int fisrtShow = SaveValues.IsFirstShow();
            if (fisrtShow > 1)
            {
                return false;
            }

            if (fisrtShow == 0)
            {
                sessionCountReached = IsSessionCountReached(RateGameSettings.firstShowSettings);
                customEventsReached = IsCustomEventReached(RateGameSettings.firstShowSettings);
                timeSinceStartReached = IsTimeSinceStartReached(RateGameSettings.firstShowSettings);
                timeSinceOpenReached = IsTimeSinceOpenReached(RateGameSettings.firstShowSettings);
            }
            else
            {
                sessionCountReached = IsSessionCountReached(RateGameSettings.postponeSettings);
                customEventsReached = IsCustomEventReached(RateGameSettings.postponeSettings);
                timeSinceStartReached = IsTimeSinceStartReached(RateGameSettings.postponeSettings);
                timeSinceOpenReached = IsTimeSinceOpenReached(RateGameSettings.postponeSettings);
            }

            if (sessionCountReached && customEventsReached && timeSinceStartReached && timeSinceOpenReached)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Checks time since opening
        /// </summary>
        /// <param name="displayConditions">first time conditions or postpone conditions</param>
        /// <returns></returns>
        private bool IsTimeSinceOpenReached(DisplayConditions displayConditions)
        {
            if (displayConditions.useRealTime)
            {
                if (SaveValues.GetTimeSinceOpen() >= displayConditions.realTime)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks time since start
        /// </summary>
        /// <param name="displayConditions">first time conditions or postpone conditions</param>
        /// <returns></returns>
        private bool IsTimeSinceStartReached(DisplayConditions displayConditions)
        {
            if (displayConditions.useInGameTime)
            {
                if (SaveValues.GetTimeSinceStart() + Time.time >= displayConditions.gamePlayTime * 60)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks sessions count
        /// </summary>
        /// <param name="displayConditions">first time conditions or postpone conditions</param>
        /// <returns></returns>
        private bool IsSessionCountReached(DisplayConditions displayConditions)
        {
            if (displayConditions.useSessionsCount)
            {
                if (SaveValues.GetNumberOfSessions() >= displayConditions.minSessiosnCount)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Checks custom events
        /// </summary>
        /// <param name="displayConditions">first time conditions or postpone conditions</param>
        /// <returns></returns>
        private bool IsCustomEventReached(DisplayConditions displayConditions)
        {
            if (displayConditions.useCustomEvents)
            {
                if (SaveValues.GetNumberOfCustomEvents() >= displayConditions.minCustomEvents)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Loads a canvas on top of everything, sets the required dimensions the loads the popup inside canvas
        /// </summary>
        /// <param name="canvas"></param>
        private void LoadPopup(GameObject canvas, GameObject popup)
        {
            Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            int max = 1;
            if (allCanvases.Length > 0)
            {
                max = allCanvases.Max(cond => cond.sortingOrder);
            }
            Transform ratePopupCanvas = Instantiate(canvas).transform;
            ratePopupCanvas.GetComponent<Canvas>().sortingOrder = IncreaseSortingOrder(max);
            if (Screen.width > Screen.height)
            {
                ratePopupCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
            }
            else
            {
                ratePopupCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
            }

            Transform ratePopup = Instantiate(popup).transform;
            ratePopup.SetParent(ratePopupCanvas, false);
        }


        private int IncreaseSortingOrder(int oldLayer)
        {
            int result = oldLayer + 1;
            if (result > short.MaxValue)
            {
                result = short.MaxValue;
            }
            return result;
        }


        /// <summary>
        /// saves the game play time
        /// </summary>
        private void OnApplicationQuit()
        {
            SaveValues.AddTimeFromStart(Time.time);
        }


        void ShowNativeRatePopup()
        {
#if UNITY_EDITOR
            Debug.Log("Native popup works only on a real device");
#endif
#if GLEY_NATIVE_GOOGLEPLAY
            StartCoroutine(OpenReview(new ReviewManager()));
#endif

#if GLEY_NATIVE_APPSTORE
            UnityEngine.iOS.Device.RequestStoreReview();
#endif
        }

#if GLEY_NATIVE_GOOGLEPLAY
        private IEnumerator OpenReview(ReviewManager reviewManager)
        {
            var requestFlowOperation = reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                RatePopupWasClosed(PopupOptions.NativeFailed, requestFlowOperation.Error.ToString());
                yield break;
            }
            var _playReviewInfo = requestFlowOperation.GetResult();
            var launchFlowOperation = reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                RatePopupWasClosed(PopupOptions.NativeFailed, launchFlowOperation.Error.ToString());
                yield break;
            }
            RatePopupWasClosed(PopupOptions.NativeSuccess, null);
        }
#endif
    }
}