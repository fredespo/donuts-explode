using UnityEngine;

namespace Gley.RateGame.Internal
{
    public class SaveValues
    {
        private const string NrOfSessionsFile = "Gley.RateGame.NrOfSessions";
        private const string NrOfCustomEventsFile = "Gley.RateGame.NrOfEvents";
        private const string FirstShowFile = "Gley.RateGame.FirstShow";
        private const string TimeSinceStart = "Gley.RateGame.TimeSinceStart";
        private const string OpenTime = "Gley.RateGame.TimeSinceOpen";


        public static void DeleteAll()
        {
            PlayerPrefs.DeleteKey(NrOfSessionsFile);
            PlayerPrefs.DeleteKey(NrOfCustomEventsFile);
            PlayerPrefs.DeleteKey(FirstShowFile);
            PlayerPrefs.DeleteKey(TimeSinceStart);
            PlayerPrefs.DeleteKey(OpenTime);
        }

        /// <summary>
        /// Increase sessions count and store them in player prefs
        /// </summary>
        public static void IncreaseNumberOfSessions()
        {
            int nrOfSessions = GetNumberOfSessions();
            nrOfSessions++;
            PlayerPrefs.SetInt(NrOfSessionsFile, nrOfSessions);
        }


        /// <summary>
        /// Get number of saved sessions
        /// </summary>
        /// <returns>Number of sessions</returns>
        public static int GetNumberOfSessions()
        {
            if (PlayerPrefs.HasKey(NrOfSessionsFile))
            {
                return PlayerPrefs.GetInt(NrOfSessionsFile);
            }
            return 0;
        }


        /// <summary>
        /// Checks if it is first run of the game
        /// </summary>
        /// <returns></returns>
        public static int IsFirstShow()
        {
            if (PlayerPrefs.HasKey(FirstShowFile))
            {
                return PlayerPrefs.GetInt(FirstShowFile);
            }
            return 0;
        }


        /// <summary>
        /// Reset all counters after a popup was seen 
        /// </summary>
        public static void MarkAsSeen()
        {
            PlayerPrefs.SetInt(FirstShowFile, 1);
            PlayerPrefs.SetInt(NrOfCustomEventsFile, 0);
            PlayerPrefs.SetInt(NrOfSessionsFile, 0);
            PlayerPrefs.SetFloat(TimeSinceStart, 0);
            SetOpenTime(true);
        }


        /// <summary>
        /// Mark popup as unseen
        /// </summary>
        public static void MarkAsUnseen()
        {
            PlayerPrefs.SetInt(FirstShowFile, 0);
        }


        /// <summary>
        /// Mark popup as never show
        /// </summary>
        public static void NeverShowPopup()
        {
            PlayerPrefs.SetInt(FirstShowFile, 2);
        }


        /// <summary>
        /// Increase and store the number of custom events
        /// </summary>
        public static void IncreaseNumberOfCustomEvents()
        {
            int nrOfEvents = GetNumberOfCustomEvents();
            nrOfEvents++;
            PlayerPrefs.SetInt(NrOfCustomEventsFile, nrOfEvents);
        }


        /// <summary>
        /// Get the number of stored custom events
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfCustomEvents()
        {
            if (PlayerPrefs.HasKey(NrOfCustomEventsFile))
            {
                return PlayerPrefs.GetInt(NrOfCustomEventsFile);
            }
            return 0;
        }


        /// <summary>
        /// Ads the amount of session time to total game play time
        /// </summary>
        /// <param name="time">session time</param>
        public static void AddTimeFromStart(float time)
        {
            float timeFromStart = GetTimeSinceStart();
            timeFromStart += time;
            PlayerPrefs.SetFloat(TimeSinceStart, timeFromStart);
        }


        /// <summary>
        /// Get time since start
        /// </summary>
        /// <returns></returns>
        public static float GetTimeSinceStart()
        {
            if (PlayerPrefs.HasKey(TimeSinceStart))
            {
                return PlayerPrefs.GetFloat(TimeSinceStart);
            }
            return 0;
        }


        /// <summary>
        /// Get time since first open of the app
        /// </summary>
        /// <returns></returns>
        public static double GetTimeSinceOpen()
        {
            if (PlayerPrefs.HasKey(OpenTime))
            {
                long temp = System.Convert.ToInt64(PlayerPrefs.GetString(OpenTime));
                System.DateTime oldDate = System.DateTime.FromBinary(temp);
                System.DateTime currentDate = System.DateTime.Now;
                System.TimeSpan difference = currentDate.Subtract(oldDate);
                return difference.TotalHours;
            }
            return 0;
        }


        /// <summary>
        /// Set first time open time
        /// </summary>
        /// <param name="reset"></param>
        public static void SetOpenTime(bool reset)
        {
            if (!PlayerPrefs.HasKey(OpenTime) || reset)
            {
                PlayerPrefs.SetString(OpenTime, System.DateTime.Now.ToBinary().ToString());
            }
        }

    }
}