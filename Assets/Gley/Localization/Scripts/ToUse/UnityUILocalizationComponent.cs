namespace Gley.Localization
{
    using Gley.Localization.Internal;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnityUILocalizationComponent : MonoBehaviour, ILocalizationComponent
    {

        public WordIDs wordID;

        /// <summary>
        /// Used for automatically refresh
        /// </summary>
        public void Refresh()
        {
            GetComponent<Text>().text = Gley.Localization.API.GetText(wordID);
        }

        void Start()
        {
            Refresh();
        }
    }
}
