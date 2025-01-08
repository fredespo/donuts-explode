#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;
using UnityEngine;

namespace Gley.Notifications.Internal
{
    [IncludeInSettings(true)]
    public static class NotificationsUVS
    {
        private static GameObject _eventTarget;

        public static void Initialize()
        {
            API.Initialize();
        }

        public static void SendNotification(string title, string text, int hours, int min, int sec, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            API.SendNotification(title, text, new System.TimeSpan(hours, min, sec), smallIcon, largeIcon, customData);
        }

        public static void SendRepeatNotification(string title, string text, int hours, int min, int sec, int repeatHours, int repeatMin, int repoeatSec, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            API.SendRepeatNotification(title, text, new System.TimeSpan(hours, min, sec), new System.TimeSpan(repeatHours, repeatMin, repoeatSec), smallIcon, largeIcon, customData);
        }

        public static void SendBigPictureNotification(string title, string text, string summaryText, int hours, int min, int sec, string bigPicturePath, bool showWhenColapsed, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            API.SendBigPictureNotification(title, text, summaryText, new System.TimeSpan(hours, min, sec), bigPicturePath, showWhenColapsed, smallIcon, largeIcon, customData);
        }

        public static string AppWasOpenFromNotification()
        {
            return API.AppWasOpenFromNotification();
        }

        public static bool IsPermissionGranted()
        {
            return API.IsPermissionGranted();
        }

        public static void CopyBigPictureToDevice(string imageName)
        {
            API.CopyBigPictureToDevice(imageName);
        }
        public static void CancelAllNotifications()
        {
            API.CancelAllNotifications();
        }

        public static void RequestPermision(GameObject eventTarget)
        {
            _eventTarget = eventTarget;
            API.RequestPermision(PermissionGranted);
        }

        private static void PermissionGranted(bool hasPermission, string message)
        {
            CustomEvent.Trigger(_eventTarget, "PermissionGranted", hasPermission,message);
        }
    }
}
#endif