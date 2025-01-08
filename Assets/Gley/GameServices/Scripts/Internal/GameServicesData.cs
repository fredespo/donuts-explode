using System.Collections.Generic;
using UnityEngine;

namespace Gley.GameServices.Internal
{
    //used to set up achievements and leaderboards properties
    public class GameServicesData : ScriptableObject
    {
        public bool useForAndroid;
        public bool useForIos;
        public bool usePlaymaker;
        public string googleAppId;
        public List<Achievement> allGameAchievements = new List<Achievement>();
        public List<Leaderboard> allGameLeaderboards = new List<Leaderboard>();
        public bool useUVS;
    }
}
