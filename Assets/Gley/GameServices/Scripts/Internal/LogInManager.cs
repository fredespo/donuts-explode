using UnityEngine;
using UnityEngine.Events;

#if GLEY_GAMESERVICES_ANDROID
using GooglePlayGames;
#endif


namespace Gley.GameServices.Internal
{
    public class LogInManager
    {
        bool login = false;
        bool activated = false;

        /// <summary>
        /// Authenticate the user for Android and iOS using Unity Social interface
        /// </summary>
        /// <param name="LoginComplete">callback -> login result</param>
        public void LogiInServices(UnityAction<bool> LoginComplete = null)
        {
            if (activated == false)
            {
#if GLEY_GAMESERVICES_ANDROID
                PlayGamesPlatform.Activate();
#endif
                activated = true;
            }
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    login = true;
                    if (LoginComplete != null)
                    {
                        LoginComplete(true);
                    }
                }
                else
                {
                    if (LoginComplete != null)
                    {
                        LoginComplete(false);
                    }
                }
            });
        }

        /// <summary>
        /// check if user is logged in
        /// </summary>
        /// <returns>true of the user is logged in</returns>
        public bool IsLoggedIn()
        {
            return login;
        }
    }
}
