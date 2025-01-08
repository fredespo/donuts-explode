using Gley.Common;

namespace Gley.CrossPromo.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/Cross Promo";
        internal const string documentation = "https://gley.gitbook.io/mobile-cross-promo/";
        internal const string autoLoadScene = "Example/Scenes/AutoloadTest.unity";
        internal const string loadAndShowScene= "Example/Scenes/LoadAndShowTest.unity";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "Cross Promo - v.";

        public int MinWidth => 520;

        public int MinHeight => 520;

        public string FolderName => "CrossPromo";

        public string ParentFolder => "Gley";
    }
}