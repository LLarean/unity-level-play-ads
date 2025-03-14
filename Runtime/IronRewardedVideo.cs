using System;
using UnityEngine;

namespace LevelPlayAds
{
    public class IronRewardedVideo
    {
        public static String REWARDED_INSTANCE_ID = "0";
        
        private event Action _reward;

        public IronRewardedVideo()
        {
            Debug.Log("IronAds: ShowRewardedVideoScript Start called");

            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        }

        public void Show(Action reward)
        {
            Debug.Log("IronAds: ShowRewardedVideoButtonClicked");
            
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                _reward = reward;
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                Debug.Log("IronAds: IronSource.Agent.isRewardedVideoAvailable - False");
            }
        }

        private void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("IronAds: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("IronAds: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
        {
            Debug.Log("IronAds: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdUnavailable()
        {
            Debug.Log("IronAds: I got RewardedVideoOnAdUnavailable");
        }

        private void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
        {
            Debug.Log("IronAds: I got RewardedVideoAdOpenedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
        {
            Debug.Log("IronAds: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
            _reward?.Invoke();
        }

        private void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
        {
            Debug.Log("IronAds: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
        }
    }
}