using UnityEngine;
using UnityEngine.UI;

namespace Gley.Notifications.Internal
{
    public class NotificationsExample : MonoBehaviour
    {
        public Text openText;
        public InputField input;

        bool sendNotification;
        int notificationTime;
        bool sendRepeatNotification;
        int repeatNotificationTime;
        bool sendBigPictureNotification;
        int bigPictureNotificationTime;
        void Start()
        {
            Gley.Notifications.API.Initialize();
            //Gley.Notifications.API.CopyBigPictureToDevice("image.jpg");
        }


        /// <summary>
        /// Attach to a UI Button
        /// </summary>
        public void RequestPermission()
        {
            Gley.Notifications.API.RequestPermision(PermissionGranted);
        }


        /// <summary>
        /// </summary>
        /// <param name="status">indicates whether the user granted or denied permission</param>
        private void PermissionGranted(bool hasPermission, string message)
        {
            if (hasPermission)
            {
                Debug.Log("Permission Granted");
            }
            else
            {
                //something else happen, check the message for more info
                Debug.Log(message);
            }
        }


        /// <summary>
        /// Associated with UI button 
        /// </summary>
        public void SendNotification()
        {
            int.TryParse(input.text, out notificationTime);
            if (notificationTime > 0)
            {
                sendNotification = true;
            }
        }


        /// <summary>
        /// Associated with UI button 
        /// </summary>
        public void SendRepeatNotification()
        {
            int.TryParse(input.text, out repeatNotificationTime);
            if (repeatNotificationTime > 0)
            {
                sendRepeatNotification = true;
            }
        }


        /// <summary>
        /// Associated with UI button
        /// </summary>
        public void SendBigPictureNotification()
        {
            int.TryParse(input.text, out bigPictureNotificationTime);
            if (bigPictureNotificationTime > 0)
            {
                sendBigPictureNotification = true;
            }
        }


        /// <summary>
        /// The best way to schedule notifications is from OnApplicationFocus method
        /// when this is called user left your app
        /// when you trigger notifications when user is still in app, maybe your notification will be delivered when user is still inside the app and that is not good practice  
        /// </summary>
        /// <param name="focus"></param>
        private void OnApplicationFocus(bool focus)
        {
            if (focus == false)
            {
                Gley.Notifications.API.SendNotification("Game Title", "App was minimized", new System.TimeSpan(0, 0, 30), "icon_0", "icon_1", "Opened from Gley Minimized Notification");
                if (sendNotification)
                {
                    Gley.Notifications.API.SendNotification("Game Title", "Notification", new System.TimeSpan(0, notificationTime, 0), "icon_0", "icon_1", "Opened from Gley Notification");
                }
                if (sendRepeatNotification)
                {
                    Gley.Notifications.API.SendRepeatNotification("Game Title", "Repeat Notification", new System.TimeSpan(0, repeatNotificationTime, 0), new System.TimeSpan(0, 0, 30), "icon_0", "icon_1", "Opened from Gley Repeat Notification");
                }
                if (sendBigPictureNotification)
                {
                    //Gley.Notifications.API.SendBigPictureNotification("Game Title", "Big Picture Notification", "Summary", new System.TimeSpan(0, bigPictureNotificationTime, 0), System.IO.Path.Combine(Application.persistentDataPath, "image.jpg"), false, "icon_0", "icon_1", "Opened from Gley Big Picture Notification");
                }
            }
            else
            {
                openText.text = Gley.Notifications.API.AppWasOpenFromNotification();
                //call initialize when user returns to your app to cancel all pending notifications
                Gley.Notifications.API.CancelAllNotifications();
            }
        }
    }
}

