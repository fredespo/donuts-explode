using Gley.Common;
namespace Gley.About
{
    public class AboutWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/About";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "About - v.";

        public int MinWidth => 600;

        public int MinHeight => 520;

        public string FolderName => "About";

        public string ParentFolder => "Gley";
    }
}