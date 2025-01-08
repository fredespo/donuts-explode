using Gley.DailyRewards;
using Gley.EasyIAP;
#if GLEY_JUMPY
using Gley.Localization;
using System;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Gley.Jumpy
{
    /// <summary>
    /// Controls the Title Screen UI
    /// </summary>
    public class TitleScreenPopup : GenericPopup
    {
        public Image soundButton;
        public Sprite onTexture;
        public Sprite offTexture;
        public GameObject restoreButton;
        public Text playText;
        public Text restoreText1;
        public Text restoreText2;
        public Text languageText;


        /// <summary>
        /// Refreshes the state of the UI buttons 
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            Gley.DailyRewards.API.TimerButton.AddClickListener(RewardButtonClicked);

#if !UNITY_IOS
            restoreButton.SetActive(false);
#endif

            RefreshTexts();

            UpdateButtons();
        }

        private void RewardButtonClicked(TimerButtonIDs button, bool timeExpired)
        {
            GameStatus.IncreaseLives();
        }

        void RefreshTexts()
        {
#if GLEY_JUMPY
            playText.text = Gley.Localization.API.GetText(WordIDs.PlayID);
            restoreText1.text = Gley.Localization.API.GetText(WordIDs.RestoreID).ToUpper();
            restoreText2.text = Gley.Localization.API.GetText(WordIDs.PurchasesID).ToUpper();
            languageText.text = Gley.Localization.API.GetCurrentLanguage().ToString();
#endif
        }


        /// <summary>
        /// Handles the button click actions
        /// </summary>
        /// <param name="button">the gameObject that was clicked</param>
        public override void PerformClickActionsPopup(GameObject button)
        {
            base.PerformClickActionsPopup(button);
            if (button.name == "PlayButton")
            {
                Gley.CrossPromo.API.ShowCrossPromoPopup();
                ClosePopup(false, () =>
                {
                    AssetsLoader.LoadInterface(GameInterfeces.InGameInterface);
                    LevelManager.Instance.RestartLevel();
                });
            }

            if (button.name == "SoundButton")
            {
                if (GameStatus.FXVolume == 0)
                {
                    GameStatus.FXVolume = 1;
                }
                else
                {
                    GameStatus.FXVolume = 0;
                }
                UpdateButtons();
            }

            if (button.name == "RestorePurchases")
            {
                DisablePopup();
                Gley.EasyIAP.API.RestorePurchases(PurchasesRestored);
            }

            if(button.name == "LanguageButton")
            {
                Gley.Localization.API.NextLanguage();
                RefreshTexts();
            }
        }


        /// <summary>
        /// Called after restore purchases process is complete
        /// </summary>
        /// <param name="status">the status of the process</param>
        /// <param name="message">the error message</param>
        /// <param name="product">the product</param>
        private void PurchasesRestored(IAPOperationStatus status, string message, StoreProduct product)
        {
#if GLEY_JUMPY
            if (status == IAPOperationStatus.Success)
            {
                if (product.productName == ShopProductNames.RemoveAds.ToString())
                {
                    if (product.active == true)
                    {
                        Gley.MobileAds.API.RemoveAds(true);
                    }
                }
            }
#endif
            EnablePopup();
        }


        /// <summary>
        /// Updates the sound settings and changes the Sound Button sprite accordingly
        /// </summary>
        private void UpdateButtons()
        {
            if (GameStatus.FXVolume == 0)
            {
                soundButton.sprite = offTexture;
            }
            else
            {
                soundButton.sprite = onTexture;
            }
            GameStatus.MusicVolume = GameStatus.FXVolume;
            SoundLoader.SetFXVoulme(GameStatus.FXVolume);
            SoundLoader.SetMusicVoulme(GameStatus.MusicVolume);
            GameStatus.SaveGameStatus();
        }
    }
}
