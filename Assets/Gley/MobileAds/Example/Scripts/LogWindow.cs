using UnityEngine;
using UnityEngine.UI;

namespace Gley.MobileAds.Internal
{
    public class LogWindow : MonoBehaviour
    {
        [SerializeField] private Text logText;
        bool showWindow;

        private void OnEnable()
        {
            if (showWindow == false)
            {
                gameObject.SetActive(false);
            }
            GleyLogger.onLogUpdate = ShowLogs;
        }

        public void HideLogsWindow()
        {
            showWindow = false;
            gameObject.SetActive(false);
        }

        public void ShowLogWindow()
        {
            showWindow = true;
            ShowLogs();
            gameObject.SetActive(true);
        }

        public void ClearLogs()
        {
            GleyLogger.ClearLogs();
        }

        void ShowLogs()
        {
            logText.text = GleyLogger.GetLogs();
        }
    }
}
