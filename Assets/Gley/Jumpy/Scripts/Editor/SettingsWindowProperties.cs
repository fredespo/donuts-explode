using Gley.Common;

namespace Gley.Jumpy.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        internal const string menuItem = "Tools/Gley/Jumpy";
        internal const string GLEY_JUMPY = "GLEY_JUMPY";
        internal const string documentation = "https://gley.gitbook.io/mobile-tools/";
        internal const string SETTINGS = "Settings";
        internal const string gameScene = "Scenes/Game.unity";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "Jumpy - v.";

        public int MinWidth => 520;

        public int MinHeight => 520;

        public string FolderName => "Jumpy";

        public string ParentFolder => "Gley";
    }
}