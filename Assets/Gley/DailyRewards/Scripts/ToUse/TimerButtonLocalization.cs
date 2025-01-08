#if GLEY_LOCALIZATION
using Gley.Localization;
#endif
using UnityEngine;

namespace Gley.DailyRewards
{
    public class TimerButtonLocalization : MonoBehaviour
    {
#if GLEY_LOCALIZATION
        public WordIDs ID;
#endif
        public string GetText()
        {
#if GLEY_LOCALIZATION
            return Localization.API.GetText(ID);
#else
            return null;
#endif
        }
    }
}