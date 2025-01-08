using Gley.DailyRewards.Internal;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gley.DailyRewards.API
{
    public static class Calendar
    {
        /// <summary>
        /// Show the Calendar Popup
        /// </summary>
        public static void Show()
        {
            CalendarManager.Instance.ShowCalendar();
        }


        /// <summary>
        /// Register a click listener that will be triggered when a calendar day is clicked
        /// </summary>
        /// <param name="clickListener"></param>
        public static void AddClickListener(UnityAction<int, int, Sprite> clickListener)
        {
            CalendarManager.Instance.AddClickListener(clickListener);
        }


        /// <summary>
        /// Returns the remaining time until current day is available to claim 
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetRemainingTimeSpan()
        {
            return CalendarManager.Instance.GetRemainingTimeSpan();
        }


        /// <summary>
        /// Resets Calendar to first day 
        /// </summary>
        public static void Reset()
        {
            CalendarManager.Instance.ResetCalendar();
        }


        public static void SetValueFormatter(ValueFormatterFunction aFunction)
        {
            CalendarManager.Instance.SetValueFormatter(aFunction);
        }


        /// <summary>
        /// Advanced to next day even if time did not passed. Useful for rewarded video.
        /// </summary>
        public static void NextDay()
        {
            CalendarManager.Instance.NextDay();
        }
    }
}