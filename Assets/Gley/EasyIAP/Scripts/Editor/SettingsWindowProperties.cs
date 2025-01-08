using Gley.Common;

namespace Gley.EasyIAP.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/Easy IAP";

        public const string GLEY_IAP_IOS = "GLEY_IAP_IOS";
        public const string GLEY_IAP_GOOGLEPLAY = "GLEY_IAP_GOOGLEPLAY";
        public const string GLEY_IAP_AMAZON = "GLEY_IAP_AMAZON";
        public const string GLEY_IAP_MACOS = "GLEY_IAP_MACOS";
        public const string GLEY_IAP_WINDOWS = "GLEY_IAP_WINDOWS";
        public const string GLEY_IAP_VALIDATION = "GLEY_IAP_VALIDATION";
        internal const string exampleScene = "Example/Scenes/EasyIAPExample.unity";
        internal const string testScene = "Example/Scenes/EasyIAPTest.unity";
        internal const string documentation = "https://gley.gitbook.io/easy-iap/";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "Easy IAP - v.";

        public int MinWidth => 520;

        public int MinHeight => 520;

        public string FolderName => "EasyIAP";

        public string ParentFolder => "Gley";
    }
}
