using System;
using com.unity3d.mediation;
using UnityEngine;

namespace LevelPlayAds
{
    public class IronInit
    {
        public static IronInit Instance;
        public static string UniqueUserId = "demoUserUnity";
        
        public IronInterstitial Inter;
        public IronRewardedVideo Rewarded;
        
        private LevelPlayBannerAd _bannerAd;

#if UNITY_ANDROID
        private readonly string _appKey = "unknown";
        private readonly string _bannerAdUnitId = "unknown";
#elif UNITY_IPHONE
        private readonly string _appKey = "unknown";
        private readonly string _bannerAdUnitId = "unknown";
#else
        private readonly string _appKey = "unexpected_platform";
        private readonly string _bannerAdUnitId = "unexpected_platform";
        private readonly string interstitialAdUnitId = "unexpected_platform";
#endif
        
        private event Action _initAction;

        public IronInit(bool childComplient = true, Action initAction = null)
        {
            Debug.Log("IronAds: Awake called");

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.Log("IRON INIT INSTANCE ALREADY EXISTS");
                return;
            }
            
            if (childComplient)
            {
                IronSource.Agent.setMetaData("is_deviceid_optout", "true");
                IronSource.Agent.setMetaData("is_child_directed", "true");
                IronSource.Agent.setMetaData("do_not_collect_ad_id", "true");
                IronSource.Agent.setMetaData("Google_Family_Self_Certified_SDKS","true");
            }

            IronSourceConfig.Instance.setClientSideCallbacks(true);

            string id = IronSource.Agent.getAdvertiserId();
            Debug.Log("IronAds: IronSource.Agent.getAdvertiserId : " + id);

            Debug.Log("IronAds: IronSource.Agent.validateIntegration");
            IronSource.Agent.validateIntegration();

            Debug.Log("IronAds: unity version" + IronSource.unityVersion());

            _initAction = initAction;
            Debug.Log("IronAds: LevelPlay Init");
            if (!PlayerPrefs.HasKey("uniqueUserId"))
            {
                UniqueUserId = GenerateUniqueUserID();
                PlayerPrefs.SetString("uniqueUserId", UniqueUserId);
            }
            else
            {
                UniqueUserId = PlayerPrefs.GetString("uniqueUserId");
            }

            IronSource.Agent.setUserId(UniqueUserId);
            LevelPlay.Init(_appKey, UniqueUserId,
                new[] { LevelPlayAdFormat.REWARDED, LevelPlayAdFormat.INTERSTITIAL, LevelPlayAdFormat.BANNER });

            LevelPlay.OnInitSuccess += OnInitializationCompleted;
            LevelPlay.OnInitFailed += (error => Debug.Log("Initialization error: " + error));
        }

        public string GenerateUniqueUserID()
        {
            string guid = Guid.NewGuid().ToString();
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            string uniqueUserID = $"{guid}_{timestamp}";
            return uniqueUserID;
        }

        private void LoadBanner()
        {
            // Create object
            _bannerAd = new LevelPlayBannerAd(_bannerAdUnitId, position: LevelPlayBannerPosition.BottomCenter);

            _bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
            _bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
            _bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
            _bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
            _bannerAd.OnAdClicked += BannerOnAdClickedEvent;
            _bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
            _bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
            _bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;

            _bannerAd.LoadAd();
        }

        private void OnInitializationCompleted(LevelPlayConfiguration configuration)
        {
            Debug.Log("Initialization completed");
            LoadBanner();
            InitVideoAds();
            _initAction?.Invoke();
        }

        private void InitVideoAds()
        {
            if (Inter == null)
                Inter = new IronInterstitial();
            Inter.LoadInterstitial();
            if (Rewarded == null)
                Rewarded = new IronRewardedVideo();
        }

        private void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got BannerOnAdLoadedEvent With AdInfo " + adInfo);
            _bannerAd.ShowAd();
        }

        private void BannerOnAdLoadFailedEvent(LevelPlayAdError error)
        {
            Debug.Log("IronAds: I got BannerOnAdLoadFailedEvent With Error " + error);
        }

        private void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got BannerOnAdClickedEvent With AdInfo " + adInfo);
        }

        private void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got BannerOnAdDisplayedEvent With AdInfo " + adInfo);
        }

        private void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError)
        {
            Debug.Log("IronAds: I got BannerOnAdDisplayFailedEvent With AdInfoError " + adInfoError);
        }

        private void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got BannerOnAdCollapsedEvent With AdInfo " + adInfo);
        }

        private void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got BannerOnAdLeftApplicationEvent With AdInfo " + adInfo);
        }

        private void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("IronAds: I got BannerOnAdExpandedEvent With AdInfo " + adInfo);
        }
    }
}