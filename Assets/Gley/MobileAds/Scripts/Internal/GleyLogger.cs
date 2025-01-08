using UnityEngine;

namespace Gley.MobileAds.Internal
{
    public static class GleyLogger
    {
        public delegate void LogUpdate();
        public static LogUpdate onLogUpdate;

        private static string logMessage;
        private static bool initialized;
        public static void Initialize()
        {
           initialized = true;
        }

        public static void AddLog(object message)
        {
            if (initialized)
            {
                logMessage += "\n" + message.ToString();
                Debug.Log(message.ToString());
                if(onLogUpdate != null)
                {
                    onLogUpdate();
                }
            }
        }

        public static string GetLogs()
        {
            return logMessage;
        }

        public static void ClearLogs()
        {
            logMessage = string.Empty;
            AddLog("Cleared");
        }

        public static bool IsInitialized()
        {
            return initialized;
        }
    }
}