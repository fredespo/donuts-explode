using UnityEngine;
using System;
using System.IO;
using System.Collections;

#if GLEY_NOTIFICATIONS_IOS
using Unity.Notifications.iOS;
#endif

#if GLEY_NOTIFICATIONS_ANDROID
using Unity.Notifications.Android;
#endif

namespace Gley.Notifications.Internal
{
    public class NotificationsManager : MonoBehaviour
    {
        private static NotificationsManager instance;

#if GLEY_NOTIFICATIONS_IOS
        AuthorizationRequest request;
#endif
#if GLEY_NOTIFICATIONS_ANDROID
        const string channelID = "channel_id";
        private bool initialized;
        PermissionRequest request;
#endif

        /// <summary>
        /// Static instance to access this class
        /// </summary>
        internal static NotificationsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "NotificationsManager";
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<NotificationsManager>();
                }
                return instance;
            }
        }


        /// <summary>
        /// Initializes notification channel and removes already scheduled notifications
        /// </summary>
        internal void Initialize()
        {
#if GLEY_NOTIFICATIONS_ANDROID
            if (initialized == false)
            {
                initialized = true;
                var c = new AndroidNotificationChannel()
                {
                    Id = channelID,
                    Name = "Default Channel",
                    Importance = Importance.High,
                    Description = "Generic notifications",

                };
                AndroidNotificationCenter.RegisterNotificationChannel(c);
                RequestNotificationPermision(null);
            }
#endif
        }


        internal void CancelAllNotifications()
        {
#if GLEY_NOTIFICATIONS_ANDROID
            AndroidNotificationCenter.CancelAllNotifications();
#endif
#if GLEY_NOTIFICATIONS_IOS
            iOSNotificationCenter.RemoveAllScheduledNotifications();
            iOSNotificationCenter.RemoveAllDeliveredNotifications();
#endif
        }


        /// <summary>
        /// Schedules a notification
        /// </summary>
        /// <param name="title">title of the notification</param>
        /// <param name="text">body of the notification</param>
        /// <param name="timeDelayFromNow">time to appear, calculated from now</param>
        /// <param name="smallIcon">small icon name for android only - from Mobile Notification Settings </param>
        /// <param name="largeIcon">large icon name for android only - from Mobile Notification Settings </param>
        /// <param name="customData">custom data that can be retrieved when user opens the app from notification </param>
        internal void SendNotification(string title, string text, TimeSpan timeDelayFromNow, string smallIcon, string largeIcon, string customData, TimeSpan? repeatInterval, string bigPicturePath, bool showWhenColapsed, string summaryText)
        {
#if GLEY_NOTIFICATIONS_ANDROID
            var notification = new AndroidNotification();
            notification.Title = title;
            notification.Text = text;
            if (repeatInterval != null)
            {
                notification.RepeatInterval = repeatInterval;
            }
            if (smallIcon != null)
            {
                notification.SmallIcon = smallIcon;
            }
            if (largeIcon != null)
            {
                notification.LargeIcon = largeIcon;
            }
            if (customData != null)
            {
                notification.IntentData = customData;
            }

            if (bigPicturePath != null)
            {
                var style = new BigPictureStyle();
                style.Picture = bigPicturePath;
                if (largeIcon != null)
                {
                    style.LargeIcon = largeIcon;
                }
                style.ShowWhenCollapsed = showWhenColapsed;
                style.SummaryText = summaryText;
                notification.BigPicture = style;
            }

            notification.FireTime = DateTime.Now.Add(timeDelayFromNow);

            AndroidNotificationCenter.SendNotification(notification, channelID);
#endif

#if GLEY_NOTIFICATIONS_IOS
            iOSNotificationTimeIntervalTrigger timeTrigger = new iOSNotificationTimeIntervalTrigger();

            if (repeatInterval == null)
            {
                timeTrigger.TimeInterval = timeDelayFromNow;
                timeTrigger.Repeats = false;
            }
            else
            {
                timeTrigger.TimeInterval = (TimeSpan)repeatInterval;
                timeTrigger.Repeats = true;
            }

            iOSNotification notification = new iOSNotification()
            {
                Title = title,
                Subtitle = "",
                Body = text,
                Data = customData,
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif
        }


        /// <summary>
        /// Check if app was opened from notification
        /// </summary>
        /// <returns>the custom data from notification schedule or null if the app was not opened from notification</returns>
        internal string AppWasOpenFromNotification()
        {
#if GLEY_NOTIFICATIONS_ANDROID
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (notificationIntentData != null)
            {
                return notificationIntentData.Notification.IntentData;
            }
            else
            {
                return null;
            }
#elif GLEY_NOTIFICATIONS_IOS
            iOSNotification notificationIntentData = iOSNotificationCenter.GetLastRespondedNotification();

            if (notificationIntentData != null)
            {
                return notificationIntentData.Data;
            }
            else
            {
                return null;
            }
#else
            return null;
#endif
        }


        internal void RequestNotificationPermision(UnityEngine.Events.UnityAction<bool, string> completeMethod)
        {
            StartCoroutine(RequestNotificationPermission(completeMethod));
        }


        private IEnumerator RequestNotificationPermission(UnityEngine.Events.UnityAction<bool, string> completeMethod)
        {
#if GLEY_NOTIFICATIONS_ANDROID
            request = new PermissionRequest();
            while (request.Status == PermissionStatus.RequestPending)
            {
                yield return null;
            }
            if (completeMethod != null)
            {
                completeMethod(request.Status == PermissionStatus.Allowed, request.Status.ToString());
            }
            // here use request.Status to determine users response
#elif GLEY_NOTIFICATIONS_IOS
            var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
            using (var req = new AuthorizationRequest(authorizationOption, true))
            {
                while (!req.IsFinished)
                {
                    yield return null;
                };
                request = req;
                completeMethod(req.Granted,req.ToString());
            }
#else
            yield return null;
#endif
        }


        internal bool IsPermissionGranted()
        {
#if GLEY_NOTIFICATIONS_ANDROID
            return (request.Status == PermissionStatus.Allowed);
#elif GLEY_NOTIFICATIONS_IOS
            return iOSNotificationCenter.GetNotificationSettings().AuthorizationStatus == AuthorizationStatus.Authorized;
#else
            return false;
#endif
        }


        internal void CopyImage(string streamingAssetsPath)
        {
            StartCoroutine(ReadFromStreamingAssets(streamingAssetsPath));
        }


        private IEnumerator ReadFromStreamingAssets(string streamingAssetsPath)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, streamingAssetsPath);
            byte[] result;
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
                yield return www.SendWebRequest();
                result = www.downloadHandler.data;
            }
            else
            {
                result = System.IO.File.ReadAllBytes(filePath);
            }
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, streamingAssetsPath), result);
        }
    }
}
