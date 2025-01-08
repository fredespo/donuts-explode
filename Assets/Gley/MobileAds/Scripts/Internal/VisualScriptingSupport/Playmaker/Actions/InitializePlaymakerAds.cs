using UnityEngine;

namespace Gley.MobileAds
{
    public class InitializePlaymakerAds : MonoBehaviour
    {
#if GLEY_PLAYMAKER_SUPPORT
        private void Start()
        {
            Gley.MobileAds.API.Initialize();
        }
#endif
    }
}