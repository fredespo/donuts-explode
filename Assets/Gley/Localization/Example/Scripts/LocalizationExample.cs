using UnityEngine;
using UnityEngine.UI;

namespace Gley.Localization.Internal
{
    public class LocalizationExample : MonoBehaviour
    {
        public Text languageText;
        public Text nextText;
        public Text prevText;
        public Text playText;
        public Text exitText;
        public Text saveText;
        void Start()
        {
            RefreshTexts();
        }


        /// <summary>
        /// Set localized text for each text field
        /// </summary>
        void RefreshTexts()
        {
            languageText.text = Gley.Localization.API.GetCurrentLanguage().ToString();
            nextText.text = Gley.Localization.API.GetText("NextID");//this has the same result as using the enum like bellow
            //nextText.text = GleyLocalization.Manager.GetText(WordIDs.NextID);
            prevText.text = Gley.Localization.API.GetText("PrevID");
            //prevText.text = GleyLocalization.Manager.GetText(WordIDs.PrevID);
            playText.text = Gley.Localization.API.GetText("PlayID");
            //playText.text = GleyLocalization.Manager.GetText(WordIDs.PlayID);
            exitText.text = Gley.Localization.API.GetText("ExitID");
            //exitText.text = GleyLocalization.Manager.GetText(WordIDs.ExitID);
            saveText.text = Gley.Localization.API.GetText("SaveID");
            //saveText.text = GleyLocalization.Manager.GetText(WordIDs.SaveID);
        }


        /// <summary>
        /// Assigned from editor. Changes current language to next language
        /// </summary>
        public void NextLanguage()
        {
            Gley.Localization.API.NextLanguage();
            RefreshTexts();
        }


        /// <summary>
        /// Assigned from editor. Changes current language to previous language
        /// </summary>
        public void PrevLanguage()
        {
            Gley.Localization.API.PreviousLanguage();
            RefreshTexts();
        }


        /// <summary>
        /// Save the current selected language
        /// </summary>
        public void SaveLanguage()
        {
            Gley.Localization.API.SetCurrentLanguage(Gley.Localization.API.GetCurrentLanguage());
        }
    }
}
