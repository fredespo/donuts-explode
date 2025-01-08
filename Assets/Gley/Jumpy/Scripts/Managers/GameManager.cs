using Gley.EasyIAP;
using System.Collections.Generic;
using UnityEngine;

namespace Gley.Jumpy
{
    /// <summary>
    /// The first script that is run in the game
    /// All initializations should be done here
    /// </summary>
    public class GameManager : SingleReference<GameManager>
    {
        /// <summary>
        /// A reference to all the important game properties should be put here
        /// </summary>
        private GameProgressManager gameStatus;
        public GameProgressManager GameStatus
        {
            get
            {
                if (gameStatus == null)
                {
                    gameStatus = new GameProgressManager();
                }
                return gameStatus;
            }
        }


        /// <summary>
        /// A reference of UI loader script to be used from entire game
        /// </summary>
        private AssetsLoaderManager assetsLoader;
        public AssetsLoaderManager AssetsLoader
        {
            get
            {
                if (assetsLoader == null)
                {
                    assetsLoader = gameObject.AddComponent<AssetsLoaderManager>();
                }
                return assetsLoader;
            }
        }


        /// <summary>
        /// A reference of sound loader script to be used from the entire game
        /// </summary>
        SoundLoaderManager soundLoader;
        public SoundLoaderManager SoundLoader
        {
            get
            {
                if (soundLoader == null)
                {
                    soundLoader = gameObject.AddComponent<SoundLoaderManager>();
                }
                return soundLoader;
            }
        }

        TweenManager tweenManager;
        public TweenManager Tween
        {
            get
            {
                if (tweenManager == null)
                {
                    tweenManager = gameObject.AddComponent<TweenManager>();
                }
                return tweenManager;
            }
        }


        /// <summary>
        /// All game initializations should be done here
        /// </summary>
        private void Start()
        {
            //Keep this object for the entire game session
            DontDestroyOnLoad(gameObject);

            //Keep the screen active all the time
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            //Initialize user input capabilities
            gameObject.AddComponent<UserInputManager>();

            //Load saved data
            GameStatus.LoadGameStatus();

            //Preload game UI
            AssetsLoader.PrepareGameUI();

            //Initialize sound with the previous saved values
            SoundLoader.InitializeSoundManager(gameObject, GameStatus.FXVolume, GameStatus.MusicVolume);

            //Start background music
            SoundLoader.AddMusic("Music");

            //Load ads
            Gley.MobileAds.API.Initialize();

            //Initialize IAP
            Gley.EasyIAP.API.Initialize(InitComplete);

            //Login
            Gley.GameServices.API.LogIn();

            //Cross Promo
            Gley.CrossPromo.API.Initialize(CompleteMethod);

            //Notifications
            Gley.Notifications.API.Initialize();

            //Start the game
            LoadGraphics();
        }

        private void CompleteMethod(bool arg0, string arg1)
        {
            Debug.Log(arg0 + " " + arg1);
        }


        /// <summary>
        /// Called when store initialization is complete, used to initialize local variable
        /// </summary>
        /// <param name="status">can be success or failed</param>
        /// <param name="message">the error message</param>
        /// <param name="allProducts">list with all products</param>
        private void InitComplete(IAPOperationStatus status, string message, List<StoreProduct> allProducts)
        {
#if GLEY_JUMPY
            if (status== IAPOperationStatus.Success)
            {
                for(int i=0;i<allProducts.Count;i++)
                {
                    if(allProducts[i].productName == ShopProductNames.RemoveAds.ToString())
                    {
                        if(allProducts[i].active==true)
                        {
                            //remove the ads from game
                            Gley.MobileAds.API.RemoveAds(true);
                        }
                    }
                }
            }
#endif
        }




        /// <summary>
        /// Loads the game graphic
        /// </summary>
        private void LoadGraphics()
        {
            AssetsLoader.LoadPopup(GamePopups.TitleScreenPopup, null);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus == false)
            {
                //if user left your app schedule all your notifications
                Gley.Notifications.API.SendNotification("Jumpy", "It is time to beat your high score", new System.TimeSpan(24, 0, 0), null, null, "Opened from notification");
#if GLEY_JUMPY
                //reward is ready
                Gley.Notifications.API.SendNotification("Jumpy", "Your reward is ready", Gley.DailyRewards.API.TimerButton.GetRemainingTime(Gley.DailyRewards.TimerButtonIDs.RewardButton));
#endif
            }
            else
            {
                //cancel all pending notifications when user returns to app
                Gley.Notifications.API.CancelAllNotifications();
            }
        }
    }
}
