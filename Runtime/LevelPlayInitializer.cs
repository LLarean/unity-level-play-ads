using System;
using Cysharp.Threading.Tasks;

namespace LevelPlayAds
{
    public class LevelPlayInitializer
    {
        private readonly IronInit _ads;
        
        private float _timeTillInter = 120f;
        private bool _isActive = true;
        private int _interClickCount;
        private bool _skipNextInter;

        public LevelPlayInitializer()
        {
            if (IronInit.Instance == null)
            {
                _ads = new IronInit();
            }
            else
            {
                _ads = IronInit.Instance;
            }
            
            StartCountdown().Forget();
        }

        public void ShowInter()
        {
            if (_timeTillInter < 1f)
            {
                if (!_skipNextInter)
                {
                    _ads.Inter.Show();
                }

                ResetCountdown();
            }
        }

        public void ShowRewarded(Action rewardAction)
        {
            _skipNextInter = true;
            _ads.Rewarded.Show(rewardAction);
        }

        public void ShowInterConditional()
        {
            _interClickCount++;
            
            if (_interClickCount >= 10)
            {
                _interClickCount = 0;
                ShowInter();
            }
        }

        private void ResetCountdown(float newTime = 120f)
        {
            _timeTillInter = newTime;
            Debug.Log($"Countdown reset to {_timeTillInter} seconds.");
        }
        
        private async UniTaskVoid StartCountdown()
        {
            while (_isActive == true)
            {
                if (_timeTillInter > 0)
                {
                    await UniTask.Delay(1000);
                    _timeTillInter--;
                }
                else
                {
                    await UniTask.Delay(1000);
                }
            }
        }

        private void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }
    }
}