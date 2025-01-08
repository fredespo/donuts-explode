using UnityEngine;

namespace Gley.Notifications.Internal
{
    /// <summary>
    /// Used by Settings Window
    /// </summary>
    public class NotificationsData : ScriptableObject
    {
        public bool useForIos;
        public bool useForAndroid;
        public bool usePlaymaker;
        public bool useUVS;
    }
}
