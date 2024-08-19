using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Sample
{
    /// <summary>
    /// Demonstrates how to use Google Mobile Ads app open ads.
    /// </summary>
    [AddComponentMenu("GoogleMobileAds/Samples/AppOpenAdController")]  // c# 의 애트리뷰트 기능 활용: 유니티 에디터의 "Component" 메뉴에 에 추가
    public class AppOpenAdController : MonoBehaviour
    {
        /// <summary>
        /// UI element activated when an ad is ready to show.
        /// </summary>
        public GameObject AdLoadedStatus;

        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-5296572742029352/3272427648"; // 실제 광고
        // private const string _adUnitId = "ca-app-pub-3940256099942544/9257395921"; // 테스트용 광고
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-5296572742029352/3272427648";
#else
        private const string _adUnitId = "unused";
#endif

        // App open ads can be preloaded for up to 4 hours.
        private readonly TimeSpan TIMEOUT = TimeSpan.FromHours(4);
        private DateTime _expireTime;
        private AppOpenAd _appOpenAd; // Google AdMob SDK에서 제공하는 클래스 중 하나로, 앱 오픈 광고(App Open Ads)를 관리하는 데 사용됩니다.


        private void Awake()
        {
            
                // AppStateEventNotifier.AppStateChanged 는 앱이 백그라운드로 전환될때, 포그라운드로 전환될때, 종료될때 호출됩니다.
                AppStateEventNotifier.AppStateChanged += OnAppStateChanged; 
                LoadAd();

                // RequestConfiguration requestConfiguration = new RequestConfiguration();
                // requestConfiguration.TestDeviceIds.Add("51d1febf-9944-42f9-af3a-d4a11d3755b6");
                // MobileAds.SetRequestConfiguration(requestConfiguration);
        }


        // void Start()
        // {
        //     MobileAds.Initialize(initStatus => {
        //         Debug.Log("AdMob Initialized");
        //     });
        // }

        // private void Awake()
        // {
        //     MobileAds.Initialize(initStatus =>
        //     {
        //         Debug.Log("AdMob Initialized");
        //         // Ensure initialization is complete before setting up state change listener
        //         AppStateEventNotifier.AppStateChanged += OnAppStateChanged; 
        //         LoadAd();
        //     });
        // }

 

        private void OnDestroy()
        {
            // Always unlisten to events when complete.
            AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
        }

        /// <summary>
        /// Loads the ad.
        /// </summary>
        public void LoadAd()
        {
            // Clean up the old ad before loading a new one.
            if (_appOpenAd != null)
            {
                DestroyAd();
            }
            

            Debug.Log("Loading app open ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest(); 

            AppOpenAd.Load(_adUnitId, adRequest, (AppOpenAd ad, LoadAdError error) =>
                {
                    // If the operation failed with a reason.
                    if (error != null)
                    {
                        Debug.LogError("App open ad failed to load an ad with error : "
                                        + error);
                        return;
                    }

                    // If the operation failed for unknown reasons.
                    // This is an unexpected error, please report this bug if it happens.
                    if (ad == null)
                    {
                        Debug.LogError("Unexpected error: App open ad load event fired with " +
                                       " null ad and null error.");
                        return;
                    }

                    // The operation completed successfully.
                    Debug.Log("App open ad loaded with response : " + ad.GetResponseInfo());
                    _appOpenAd = ad; // 이전에 선언한 변수가 인스턴스를 참조하게 함

                    // App open ads can be preloaded for up to 4 hours.
                    _expireTime = DateTime.Now + TIMEOUT;

                    // Register to ad events to extend functionality.
                    RegisterEventHandlers(ad);

                    // Inform the UI that the ad is ready.
                    AdLoadedStatus?.SetActive(true); 
                });
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void ShowAd()
        {
            // App open ads can be preloaded for up to 4 hours.
            if (_appOpenAd != null && _appOpenAd.CanShowAd() && DateTime.Now < _expireTime)
            {
               Debug.Log("Showing app open ad.");
                _appOpenAd.Show();
            }
            else
            {
                Debug.LogError("App open ad is not ready yet.");
            }

            // Inform the UI that the ad is not ready.
            AdLoadedStatus?.SetActive(false);

        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void DestroyAd()
        {
            if (_appOpenAd != null)
            {
                Debug.Log("Destroying app open ad.");
                _appOpenAd.Destroy();
                _appOpenAd = null;
            }

            // Inform the UI that the ad is not ready.
            AdLoadedStatus?.SetActive(false);
        }

        /// <summary>
        /// Logs the ResponseInfo.
        /// </summary>
        public void LogResponseInfo()
        {
            if (_appOpenAd != null)
            {
                var responseInfo = _appOpenAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }

        private void OnAppStateChanged(AppState state)
        {
            Debug.Log("App State changed to : " + state);

            // If the app is Foregrounded and the ad is available, show it.
            if (state == AppState.Foreground)
            {
                ShowAd();
            }
        }

        private void RegisterEventHandlers(AppOpenAd ad)
        {
            //이벤트: 광고가 수익을 발생시켰을때 호출된다. 
            //이벤트 핸들러: 수익금액과 통화코드를 로그에 기록한다
            ad.OnAdPaid += (AdValue adValue) => // adValue는 이벤트로부터 전달 받는다
            {
                Debug.Log(String.Format("App open ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            
            //이벤트: 광고가 사용자에게 표시되었을 때 호출됩니다.
            //이벤트 핸들러: 수익금액과 통화코드를 로그에 기록한다
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("App open ad recorded an impression.");
            };
            
            // 이벤트: 사용자가 광고를 클릭했을 때 호출됩니다.
            // 이벤트 핸들러: 광고 클릭 이벤트를 로그에 기록합니다.
            ad.OnAdClicked += () =>
            {
                Debug.Log("App open ad was clicked.");
            };
            
            // 이벤트: 광고가 전체 화면 콘텐츠로 열렸을 때 호출됩니다.
            // 이벤트 핸들러: 광고가 열렸음을 로그에 기록하고, 광고 로드 상태를 UI에 반영합니다.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("App open ad full screen content opened.");

                // Inform the UI that the ad is consumed and not ready.
                AdLoadedStatus?.SetActive(false);
            };
            
            // 이벤트: 광고가 전체 화면 콘텐츠에서 닫혔을 때 호출됩니다.
            // 이벤트 핸들러: 광고가 닫혔음을 로그에 기록하고, 새로운 광고를 로드합니다.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("App open ad full screen content closed.");

                // It may be useful to load a new ad when the current one is complete.
                if (ad == null)
                {
                    Debug.Log("ad go null");
                }
                else
                {
                    Debug.Log("ad not go null");
                }
                LoadAd();
            };
            
            // 이벤트: 광고가 전체 화면 콘텐츠로 열리는데 실패했을 때 호출됩니다.
            // 이벤트 핸들러: 오류 메시지를 로그에 기록합니다.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("App open ad failed to open full screen content with error : "
                                + error);
            };
        }
    }
}