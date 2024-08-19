using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;


public class RewardADAluminum : MonoBehaviour
{
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-5296572742029352/8886644578";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        private const string _adUnitId = "unused";
#endif

    public static RewardADAluminum instance;
    public RewardedAd _rewardedAd;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
            LoadAd();
        }
        else
        {
            Destroy(gameObject);
        }
    }

        public void LoadAd()
        {
            if (_rewardedAd != null)
            {
                DestroyAd();
            }
            var adRequest = new AdRequest();

            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    return;
                }

                if (ad == null)
                {
                    return;
                }

                // Debug.Log("Rewarded ad loaded with response");
                _rewardedAd = ad;

            });
        }
        public void DestroyAd()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
        }

        public void ShowAd()
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) => 
                {
                   EventManager.instance.TriggerWatchedRewardAd("bat");
                });

            }
        }
}
