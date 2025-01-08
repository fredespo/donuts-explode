using Gley.DailyRewards.Internal;
using System;
using UnityEngine.Events;

namespace Gley.DailyRewards.API
{
    public static class TimerButton
    {
        /// <summary>
        /// Register a click listener that will be triggered when a timer button is clicked
        /// </summary>
        /// <param name="clickListener">required params: (timerButtonID, timerExpired)</param>
        public static void AddClickListener(UnityAction<TimerButtonIDs, bool> clickListener)
        {
            TimerButtonManager.Instance.AddClickListener(clickListener);
        }


        /// <summary>
        /// Get remaining timespan for the specified button
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static TimeSpan GetRemainingTime(TimerButtonIDs button)
        {
            return TimerButtonManager.Instance.GetRemainingTimeSpan(button);
        }


        /// <summary>
        /// Reset timer for a specific button
        /// </summary>
        /// <param name="button">button ID from settings window</param>
        public static void ResetTimer(TimerButtonIDs button)
        {
            TimerButtonManager.Instance.ResetTimer(button);
        }


        /// <summary>
        /// Add the amount of time to the specified button
        /// </summary>
        /// <param name="button"></param>
        /// <param name="timeToAdd"></param>
        public static void AddTime(TimerButtonIDs button, TimeSpan timeToAdd)
        {
            TimerButtonManager.Instance.ModifyTime(button, timeToAdd, false);
        }


        /// <summary>
        /// Subtract the amount of time from the specified button
        /// </summary>
        /// <param name="button"></param>
        /// <param name="timeToRemove"></param>
        public static void RemoveTime(TimerButtonIDs button, TimeSpan timeToRemove)
        {
            TimerButtonManager.Instance.ModifyTime(button, timeToRemove, true);
        }
    }
}
