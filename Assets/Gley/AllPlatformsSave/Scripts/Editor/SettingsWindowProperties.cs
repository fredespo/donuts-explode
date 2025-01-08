using Gley.Common;

namespace Gley.AllPlatformsSave.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/All Platforms Save";
        public const string testScene = "Example/Scenes/AllPlatformsSaveExample.unity";
        public const string documentation = "https://gley.gitbook.io/all-platforms-save/";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "All Platforms Save - v.";

        public int MinWidth => 520;

        public int MinHeight => 520;

        public string FolderName => "AllPlatformsSave";

        public string ParentFolder => "Gley";
    }
}
