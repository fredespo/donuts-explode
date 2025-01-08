#if GLEY_NGUI_LOCALIZATION
using Gley.Localization.Internal;
using UnityEngine;

namespace Gley.Localization
{
    public class NGUILocalizationComponent : MonoBehaviour, ILocalizationComponent
    {
        public WordIDs wordID;

        /// <summary>
        /// Used for automatically refresh
        /// </summary>
        public void Refresh()
        {
            GetComponent<UILabel>().text = LocalizationManager.Instance.GetText(wordID);
        }

        void Start()
        {
            Refresh();
        }
    }
}
#endif
