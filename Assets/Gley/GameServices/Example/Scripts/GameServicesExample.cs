using UnityEngine;

namespace Gley.GameServices.Internal
{
    public class GameServicesExample : MonoBehaviour
    {
        public void Login()
        {
            Gley.GameServices.API.LogIn();
        }


        public void SubmitAchievement()
        {
            //you call should look like this, but we commented it because you will get errors if you define your own achievements
            //Gley.GameServices.API.SubmitAchievement(AchievementNames.LowJumper);

            //submit the first achievement from settings window
            Gley.GameServices.API.SubmitAchievement(0);
        }


        public void ShowAchievementsUI()
        {
            Gley.GameServices.API.ShowAchievementsUI();
        }


        long score = 100;
        public void SubmitScore()
        {
            //you call should look like this, but we commented it because you will get errors if you define your own LEADERBOARDS
            //Gley.GameServices.API.SubmitScore(score, LeaderboardNames.HighestJumpers);

            Gley.GameServices.API.SubmitScore(score, 0);
        }


        public void ShowLeaderboardsUI()
        {
            Gley.GameServices.API.ShowLeaderboadsUI();
        }
    }
}

