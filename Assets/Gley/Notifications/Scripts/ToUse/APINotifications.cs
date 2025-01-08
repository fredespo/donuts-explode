using Gley.Notifications.Internal;
using UnityEngine.Events;

namespace Gley.Notifications
{
    public static class API
    {
        /// <summary>
        /// Creates notification channel with possibility to cancel or keep pending notifications
        /// Call it at the beginning of your app 
        /// </summary>
        public static void Initialize()
        {
            NotificationsManager.Instance.Initialize();
        }


        /// <summary>
        /// Schedule a notification
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="text">Content of the notification</param>
        /// <param name="timeDelayFromNow">Delay to display the notification, this delay will be added to current time</param>
        /// <param name="smallIcon">Name of the custom small icon from Mobile Notification Settings</param>
        /// <param name="largeIcon">Name of the custom large icon from Mobile Notification Settings</param>
        /// <param name="customData">This data can be retrieved if the users opens app from notification</param>
        public static void SendNotification(string title, string text, System.TimeSpan timeDelayFromNow, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            NotificationsManager.Instance.SendNotification(title, text, timeDelayFromNow, smallIcon, largeIcon, customData, null, null, default, default);
        }


        /// <summary>
        /// Schedule a notification that repeats periodically 
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="text">Content of the notification</param>
        /// <param name="timeDelayFromNow">Delay to display the notification, this delay will be added to current time</param>
        /// <param name="repeatInterval">Time until the next notifications will be sent</param>
        /// <param name="smallIcon">Name of the custom small icon from Mobile Notification Settings</param>
        /// <param name="largeIcon">Name of the custom large icon from Mobile Notification Settings</param>
        /// <param name="customData">This data can be retrieved if the users opens app from notification</param>
        public static void SendRepeatNotification(string title, string text, System.TimeSpan timeDelayFromNow, System.TimeSpan? repeatInterval, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            NotificationsManager.Instance.SendNotification(title, text, timeDelayFromNow, smallIcon, largeIcon, customData, repeatInterval, null, default, default);
        }


        /// <summary>
        /// Schedule a big picture notification.
        /// Only available on Android.
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="text">Content of the notification</param>
        /// <param name="summaryText">Summary displayed when notification is expended</param>
        /// <param name="timeDelayFromNow">Delay to display the notification, this delay will be added to current time</param>
        /// <param name="bigPicturePath">Path to the picture to be displayed</param>
        /// <param name="showWhenColapsed">When notification is collapsed, a smaller version of the big picture should be displayed if this is true. If it is false, the largeIcon will be displayed</param>
        /// <param name="smallIcon">Name of the custom small icon from Mobile Notification Settings</param>
        /// <param name="largeIcon">Name of the custom large icon from Mobile Notification Settings</param>
        /// <param name="customData">This data can be retrieved if the users opens app from notification</param>
        public static void SendBigPictureNotification(string title, string text, string summaryText, System.TimeSpan timeDelayFromNow, string bigPicturePath, bool showWhenColapsed, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            NotificationsManager.Instance.SendNotification(title, text, timeDelayFromNow, smallIcon, largeIcon, customData, null, bigPicturePath, showWhenColapsed, summaryText);
        }


        /// <summary>
        /// Schedule a big picture notification that repeats periodically 
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="text">Content of the notification</param>
        /// <param name="summaryText">Summary displayed when notification is expended</param>
        /// <param name="timeDelayFromNow">Delay to display the notification, this delay will be added to current time</param>
        /// <param name="repeatInterval">Time until the next notifications will be sent</param>
        /// <param name="bigPicturePath">Path to the picture to be displayed</param>
        /// <param name="showWhenColapsed">When notification is collapsed, a smaller version of the big picture should be displayed if this is true. If it is false, the largeIcon will be displayed</param>
        /// <param name="smallIcon">Name of the custom small icon from Mobile Notification Settings</param>
        /// <param name="largeIcon">Name of the custom large icon from Mobile Notification Settings</param>
        /// <param name="customData">This data can be retrieved if the users opens app from notification</param>
        public static void SendRepeatBigPictureNotification(string title, string text, string summaryText, System.TimeSpan timeDelayFromNow, System.TimeSpan? repeatInterval, string bigPicturePath, bool showWhenColapsed, string smallIcon = null, string largeIcon = null, string customData = "")
        {
            NotificationsManager.Instance.SendNotification(title, text, timeDelayFromNow, smallIcon, largeIcon, customData, repeatInterval, bigPicturePath, showWhenColapsed, summaryText);
        }


        /// <summary>
        /// Check if current session was opened from notification click
        /// </summary>
        /// <returns>the custom data sent to notification or null if the app was not opened from notification</returns>
        public static string AppWasOpenFromNotification()
        {
            return NotificationsManager.Instance.AppWasOpenFromNotification();
        }


        /// <summary>
        /// Request permission from the OS to send notifications.
        /// </summary>
        /// <param name="completeMethod">Callback method invoked when the user responds to the permission request</param>
        public static void RequestPermision(UnityAction<bool,string> completeMethod)
        {
            NotificationsManager.Instance.RequestNotificationPermision(completeMethod);
        }


        /// <summary>
        /// Verify whether or not the permission was granted by the user
        /// </summary>
        /// <returns>true if user granted notification permission </returns>
        public static bool IsPermissionGranted()
        {
            return NotificationsManager.Instance.IsPermissionGranted();
        }


        /// <summary>
        /// Copy an image from Streaming Assets folder to the file system
        /// </summary>
        /// <param name="imageName">The name of the image</param>
        public static void CopyBigPictureToDevice(string imageName)
        {
            NotificationsManager.Instance.CopyImage(imageName);
        }


        /// <summary>
        /// Cancel all pending notifications
        /// </summary>
        public static void CancelAllNotifications()
        {
            NotificationsManager.Instance.CancelAllNotifications();
        }
    }
}
