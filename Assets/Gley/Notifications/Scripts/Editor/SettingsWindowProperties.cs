using Gley.Common;

namespace Gley.Notifications.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/Notifications";

        public const string GLEY_NOTIFICATIONS_ANDROID = "GLEY_NOTIFICATIONS_ANDROID";
        public const string GLEY_NOTIFICATIONS_IOS = "GLEY_NOTIFICATIONS_IOS";
        internal const string notificationExample = "Example/Scenes/NotificationsExample.unity";
        internal static string documentation= "https://gley.gitbook.io/mobile-notifications/";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "Notifications - v.";

        public int MinWidth => 520;

        public int MinHeight => 520;

        public string FolderName => "Notifications";

        public string ParentFolder => "Gley";
    }
}