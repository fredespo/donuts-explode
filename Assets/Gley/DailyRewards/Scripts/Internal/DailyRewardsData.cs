using System.Collections.Generic;
using UnityEngine;

namespace Gley.DailyRewards.Internal
{
    /// <summary>
    /// Contains all asset settings
    /// </summary>
    public class DailyRewardsData : ScriptableObject
    {
        public List<TimerButtonProperties> allTimerButtons = new List<TimerButtonProperties>();
        public GameObject calendarPrefab;
        public GameObject calendarCanvas;
        public bool availableAtStart = true;
        public bool resetAtEnd = false;
        public int hours = 24;
        public int minutes = 0;
        public int seconds = 0;
        public List<CalendarDayProperties> allDays = new List<CalendarDayProperties>();
        public bool usePlaymaker;
        public bool useUVS;
    }
}