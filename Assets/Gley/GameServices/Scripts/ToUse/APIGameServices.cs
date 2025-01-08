using Gley.GameServices.Internal;
using UnityEngine.Events;

namespace Gley.GameServices
{
    public class API
    {
        /// <summary>
        /// Login the player, should be called just once at the beginning of your game
        /// </summary>
        /// <param name="loginComplete">Callback called after login process ends.</param>
        public static void LogIn(UnityAction<bool> loginComplete = null)
        {
            GameServicesManager.Instance.LogIn(loginComplete);
        }


        /// <summary>
        /// Submit an achievement
        /// </summary>
        /// <param name="achievementName">AchievementNames is an enum auto-generated when you use the SettingsWindow to set up the plugin
        /// It contains all your achievements so it is easy for you to call it from your code</param>
        /// <param name="submitComplete">callback if bool is true, achievement submitted successfully, else you get an error message</param>
        public static void SubmitAchievement(AchievementNames achievementName, UnityAction<bool, GameServicesError> submitComplete = null)
        {
            GameServicesManager.Instance.SubmitAchievement(achievementName, submitComplete);
        }


        /// <summary>
        /// Submit an incremental achievement
        /// </summary>
        /// <param name="achievementName">AchievementNames is an enum auto-generated when you use the SettingsWindow to set up the plugin</param>
        /// <param name="steps">how many units to be incremented</param>
        /// <param name="submitComplete">callback if bool is true, achievement submitted successfully, else you get an error message</param>
        public static void IncrementAchievement(AchievementNames achievementName, int steps, UnityAction<bool, GameServicesError> submitComplete = null)
        {
            GameServicesManager.Instance.IncrementAchievement(achievementName, steps, submitComplete);
        }


        /// <summary>
        /// Shows the default list of all game achievements
        /// </summary>
        public static void ShowAchievementsUI()
        {
            GameServicesManager.Instance.ShowAchievementsUI();
        }


        /// <summary>
        /// Submits the score a specific leaderboard
        /// </summary>
        /// <param name="score">The player's score</param>
        /// <param name="leaderboardName">LeaderboardsNames is an enum with all game leaderboards automatically generated from SettingsWindow</param>
        /// <param name="submitComplete">callback if bool is true score was submitted successfully else you get en error messages</param>
        public static void SubmitScore(long score, LeaderboardNames leaderboardName, UnityAction<bool, GameServicesError> submitComplete = null)
        {
            GameServicesManager.Instance.SubmitScore(score, leaderboardName, submitComplete);
        }


        /// <summary>
        /// Shows all game leaderboards
        /// </summary>
        public static void ShowLeaderboadsUI()
        {
            GameServicesManager.Instance.ShowLeaderboadsUI();
        }


        /// <summary>
        /// Shows a single game Leaderboard
        /// </summary>
        /// <param name="leaderboardName">The name from Settings Window of the Leaderboard to display</param>
        public static void ShowSpecificLeaderboard(LeaderboardNames leaderboardName)
        {
            GameServicesManager.Instance.ShowSpecificLeaderboard(leaderboardName);
        }


        /// <summary>
        /// Get the highest score from leaderboard for the current user  
        /// </summary>
        /// <param name="leaderboardName">Name of the leaderboard</param>
        /// <param name="completeMethod">Method to call after score is read</param>
        public static void GetPlayerScore(LeaderboardNames leaderboardName, UnityAction<long> completeMethod)
        {
            GameServicesManager.Instance.GetPlayerScore(leaderboardName, completeMethod);
        }


        /// <summary>
        /// Get rank from leaderboard for the current user  
        /// </summary>
        /// <param name="leaderboardName">Name of the leaderboard</param>
        /// <param name="completeMethod">Method to call after rank is read</param>
        public static void GetPlayerRank(LeaderboardNames leaderboardName, UnityAction<long> completeMethod)
        {
            GameServicesManager.Instance.GetPlayerRank(leaderboardName, completeMethod);
        }


        /// <summary>
        /// Used to check if user is logged in
        /// </summary>
        /// <returns>true if logged in</returns>
        public static bool IsLoggedIn()
        {
            return GameServicesManager.Instance.IsLoggedIn();
        }


        /// <summary>
        /// Returns if an achievement is complete
        /// </summary>
        /// <param name="name">Achievement name from Settings Window</param>
        /// <returns></returns>
        public static bool IsComplete(AchievementNames name)
        {
            return GameServicesManager.Instance.IsComplete(name);
        }
    }
}