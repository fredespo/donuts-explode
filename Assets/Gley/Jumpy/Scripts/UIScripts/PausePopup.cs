#if GLEY_JUMPY
using Gley.Localization;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Gley.Jumpy
{
    /// <summary>
    /// Controls the pause UI
    /// </summary>
    public class PausePopup : GenericPopup
    {
        public Text breakText;
        public Text restartText;
        public Text closeText;

#if GLEY_JUMPY
        public override void Initialize()
        {
            base.Initialize();
            breakText.text = Gley.Localization.API.GetText(WordIDs.BreakID);
            restartText.text = Gley.Localization.API.GetText(WordIDs.RestartID);
            closeText.text = Gley.Localization.API.GetText(WordIDs.CloseID);
        }
#endif

        /// <summary>
        /// handles the button click actions
        /// </summary>
        /// <param name="button">the gameObject that was clicked</param>
        public override void PerformClickActionsPopup(GameObject button)
        {
            base.PerformClickActionsPopup(button);
            if(button.name == "RestartButton")
            {
                ClosePopup(true,()=> LevelManager.Instance.RestartLevel());
            }

            if(button.name == "CloseButton")
            {
                ClosePopup();
            }
        }
    }
}