#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;
using UnityEngine;

namespace Gley.GameServices.Internal
{
    [IncludeInSettings(true)]
    public class GameServicesUVS
    {
        static GameObject logInEventTarget;
        static GameObject achievementEventTarget;
        static GameObject leaderboardEventTarget;
        static GameObject playerScoreEventTarget;
        static GameObject playerRankEventTarget;
        public static void LogIn(GameObject _eventTarget)
        {
            logInEventTarget = _eventTarget;
            Gley.GameServices.API.LogIn(LogInComplete);
        }

        private static void LogInComplete(bool success)
        {
            CustomEvent.Trigger(logInEventTarget, "LogInComplete", false);
        }

        public static void ShowAchievementsUI()
        {
            Gley.GameServices.API.ShowAchievementsUI();
        }

        public static void ShowLeaderboardsUI()
        {
            Gley.GameServices.API.ShowLeaderboadsUI();
        }

        public static void SubmitAchievement(AchievementNames achievementName, GameObject _eventTarget)
        {
            achievementEventTarget = _eventTarget;
            Gley.GameServices.API.SubmitAchievement(achievementName, SubmitAchievementComplete);
        }

        private static void SubmitAchievementComplete(bool success, GameServicesError error)
        {
            CustomEvent.Trigger(achievementEventTarget, "SubmitAchievementComplete", success);
        }

        public static void SubmitScore(long score, LeaderboardNames leaderboardName, GameObject _eventTarget)
        {
            leaderboardEventTarget = _eventTarget;
            Gley.GameServices.API.SubmitScore(score, leaderboardName, SubmitScoreComplete);
        }

        private static void SubmitScoreComplete(bool success, GameServicesError error)
        {
            CustomEvent.Trigger(leaderboardEventTarget, "SubmitScoreComplete", success);
        }

        public static void GetPlayerScore(LeaderboardNames leaderboardName, GameObject _eventTarget)
        {
            playerScoreEventTarget = _eventTarget;
            Gley.GameServices.API.GetPlayerScore(leaderboardName, GetScoreComplete);
        }

        private static void GetScoreComplete(long score)
        {
            CustomEvent.Trigger(playerScoreEventTarget, "GetScoreComplete", score);
        }

        public static void GetPlayerRank(LeaderboardNames leaderboardName, GameObject _eventTarget)
        {
            playerRankEventTarget = _eventTarget;
            Gley.GameServices.API.GetPlayerRank(leaderboardName, GetRankComplete);
        }

        private static void GetRankComplete(long rank)
        {
            CustomEvent.Trigger(playerRankEventTarget, "GetRankComplete", rank);
        }
    }
}
#endif
