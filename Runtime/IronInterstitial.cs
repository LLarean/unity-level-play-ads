using com.unity3d.mediation;
using UnityEngine;

namespace LevelPlayAds
{
    public class IronInterstitial
    {
        public static string INTERSTITIAL_INSTANCE_ID = "0";
        
#if UNITY_ANDROID
        private readonly string _interstitialAdUnitId = "unknown";
#elif UNITY_IPHONE
        private readonly string _interstitialAdUnitId = "unknown";
#else
        private readonly string _interstitialAdUnitId = "unexpected_platform";
#endif

        private LevelPlayInterstitialAd _interstitialAd;

        public IronInterstitial()
        {
            Debug.Log("IronAds: ShowInterstitialScript Start called");
            LoadInterstitial();
        }

        public void LoadInterstitial()
        {
            _interstitialAd = new LevelPlayInterstitialAd(_interstitialAdUnitId);

            _interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
            _interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
            _interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
            _interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
            _interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
            _interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
            _interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;

            Debug.Log("IronAds: LoadInterstitialButtonClicked");
            _interstitialAd.LoadAd();
        }

        public void Show()
        {
            Debug.Log("IronAds: ShowInterstitialButtonClicked");
            if (_interstitialAd.IsAdReady())
            {
                _interstitialAd.ShowAd();
            }
            else
            {
                Debug.Log("IronAds: interstitialAd.IsAdReady - False");
            }
        }

        private void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got InterstitialOnAdLoadedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
        {
            Debug.Log("IronAds: I got InterstitialOnAdLoadFailedEvent With Error " + error);
            //LOAD IS ALREADY CALLED = 627
            if (error.ErrorCode != 627)
            {
                _interstitialAd.LoadAd();
            }
        }

        private void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got InterstitialOnAdDisplayedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError infoError)
        {
            Debug.Log("IronAds: I got InterstitialOnAdDisplayFailedEvent With InfoError " + infoError);
        }

        private void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got InterstitialOnAdInfoChangedEvent With AdInfo " + adInfo);
        }
    }
}