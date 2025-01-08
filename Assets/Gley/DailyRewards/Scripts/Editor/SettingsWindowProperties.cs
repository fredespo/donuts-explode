using Gley.Common;

namespace Gley.DailyRewards.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        internal const string menuItem = "Tools/Gley/Daily Rewards";

        internal const string GLEY_DAILY_REWARDS = "GLEY_DAILY_REWARDS";
        internal const string documentation = "https://gley.gitbook.io/daily-rewards/";
        internal const string timerButtonExample = "Example/Scenes/TimerButtonExample.unity";
        internal const string calendarExample = "Example/Scenes/CalendarExample.unity";

        public string VersionFilePath => "/Scripts/Version.txt";

        public string WindowName => "Daily Rewards - v.";

        public int MinWidth => 520;

        public int MinHeight => 520;

        public string FolderName => "DailyRewards";

        public string ParentFolder => "Gley";
    }
}