#if GLEY_ADMOB
using GoogleMobileAds.Api;
#endif

using UnityEngine.Events;

namespace Gley.MobileAds
{
    public class Events
    {
        public delegate void Initialized();
        public static event Initialized onInitialized;
        public void TriggerOnInitialized()
        {
            if (onInitialized != null)
            {
                onInitialized();
            }
        }


        public delegate void BannerLoadSucces();
        public static event BannerLoadSucces onBannerLoadSucces;
        public void TriggerBannerLoadSucces()
        {
            if (onBannerLoadSucces != null)
            {
                onBannerLoadSucces();
            }
        }


        public delegate void BannerLoadFailed(string message);
        public static event BannerLoadFailed onBannerLoadFailed;
        public void TriggerBannerLoadFailed(string message)
        {
            if (onBannerLoadFailed != null)
            {
                onBannerLoadFailed(message);
            }
        }


        public delegate void BannerClicked();
        public static event BannerClicked onBannerClicked;
        public void TriggerBannerClicked()
        {
            if (onBannerClicked != null)
            {
                onBannerClicked();
            }
        }


        public delegate void InterstitialLoadSucces();
        public static event InterstitialLoadSucces onInterstitialLoadSucces;
        public void TriggerInterstitialLoadSucces()
        {
            if (onInterstitialLoadSucces != null)
            {
                onInterstitialLoadSucces();
            }
        }


        public delegate void InterstitialLoadFailed(string message);
        public static event InterstitialLoadFailed onInterstitialLoadFailed;
        public void TriggerInterstitialLoadFailed(string message)
        {
            if (onInterstitialLoadFailed != null)
            {
                onInterstitialLoadFailed(message);
            }
        }


        public delegate void InterstitialClicked();
        public static event InterstitialClicked onInterstitialClicked;
        public void TriggerInterstitialClicked()
        {
            if (onInterstitialClicked != null)
            {
                onInterstitialClicked();
            }
        }


        public delegate void AppOpenLoadSucces();
        public static event AppOpenLoadSucces onAppOpenLoadSucces;
        public void TriggerAppOpenLoadSucces()
        {
            if (onAppOpenLoadSucces != null)
            {
                onAppOpenLoadSucces();
            }
        }


        public delegate void AppOpenLoadFailed(string message);
        public static event AppOpenLoadFailed onAppOpenLoadFailed;
        public void TriggerAppOpenLoadFailed(string message)
        {
            if (onAppOpenLoadFailed != null)
            {
                onAppOpenLoadFailed(message);
            }
        }


        public delegate void AppOpenClicked();
        public static event AppOpenClicked onAppOpenClicked;
        public void TriggerAppOpenClicked()
        {
            if (onAppOpenClicked != null)
            {
                onAppOpenClicked();
            }
        }


        public delegate void RewardedVideoLoadSucces();
        public static event RewardedVideoLoadSucces onRewardedVideoLoadSucces;
        public void TriggerRewardedVideoLoadSucces()
        {
            if (onRewardedVideoLoadSucces != null)
            {
                onRewardedVideoLoadSucces();
            }
        }


        public delegate void RewardedVideoLoadFailed(string message);
        public static event RewardedVideoLoadFailed onRewardedVideoLoadFailed;
        public void TriggerRewardedVideoLoadFailed(string message)
        {
            if (onRewardedVideoLoadFailed != null)
            {
                onRewardedVideoLoadFailed(message);
            }
        }


        public delegate void RewardedVideoClicked();
        public static event RewardedVideoClicked onRewardedVideoClicked;
        public void TriggerRewardedVideoClicked()
        {
            if (onRewardedVideoClicked != null)
            {
                onRewardedVideoClicked();
            }
        }


        public delegate void RewardedInterstitialLoadSucces();
        public static event RewardedInterstitialLoadSucces onRewardedInterstitialLoadSucces;
        public void TriggerRewardedInterstitialLoadSucces()
        {
            if (onRewardedInterstitialLoadSucces != null)
            {
                onRewardedInterstitialLoadSucces();
            }
        }


        public delegate void RewardedInterstitialLoadFailed(string message);
        public static event RewardedInterstitialLoadFailed onRewardedInterstitialLoadFailed;
        public void TriggerRewardedInterstitialLoadFailed(string message)
        {
            if (onRewardedInterstitialLoadFailed != null)
            {
                onRewardedInterstitialLoadFailed(message);
            }
        }


        public delegate void RewardedInterstitialClicked();
        public static event RewardedInterstitialClicked onRewardedInterstitialClicked;
        public void TriggerRewardedInterstitialClicked()
        {
            if (onRewardedInterstitialClicked != null)
            {
                onRewardedInterstitialClicked();
            }
        }

#if GLEY_ADMOB
        public static UnityAction<AdValue, ResponseInfo> OnBannerPaid;
        public static UnityAction<AdValue, ResponseInfo> OnInterstitialPaid;
        public static UnityAction<AdValue, ResponseInfo> OnRewardedInterstitialPaid; 
        public static UnityAction<AdValue, ResponseInfo> OnRewardedVideoPaid;
        public static UnityAction<AdValue, ResponseInfo> OnAppOpenPaid;
#endif
    }
}