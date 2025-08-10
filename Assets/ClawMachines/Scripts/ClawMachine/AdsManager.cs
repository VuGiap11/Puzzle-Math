﻿using DG.Tweening;
using GoogleMobileAds.Api;
using NTPackage.UI;
using System;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class AdsManager : MonoBehaviour
    {
        public static AdsManager instance;
        BannerView _bannerView;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;
        private RewardedInterstitialAd _rewardedInterstitialAd;
        Action Action, ActionFailBack;
        private bool _hasEarnedReward = false;
        private void Awake()
        {
            if (instance == null)
            { instance = this; }
        }
        public void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            //MobileAds.Initialize((InitializationStatus initStatus) =>
            //{
            //    // This callback is called once the MobileAds SDK is initialized.
            //    if (!UserManager.instance.useData.isRemoveAds)
            //    {
            //        LoadAd();
            //        LoadInterstitialAd();
            //    }
            //    LoadRewardedAd();
            //    LoadRewardedInterstitialAd();
            //});
            //Init();
            // MobileAds.RaiseAdEventsOnUnityMainThread = true;
        }


        public void Init()
        {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                if (!UserManager.instance.useData.isRemoveAds)
                {
                    LoadAd();
                    LoadInterstitialAd();
                }
                LoadRewardedAd();
                LoadRewardedInterstitialAd();
            });
        }

        public void RemoveAds()
        {
            if (_interstitialAd != null)
            {
                Debug.Log("Destroying interstitial ad.");
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }

        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string _adBannerUnitId = "ca-app-pub-4705307774712357/3586579102";
#elif UNITY_IPHONE
                  private string _adBannerUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
                  private string _adBannerUnitId = "unused";
#endif
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string _adInterstitialAdUnitId = "ca-app-pub-4705307774712357/2088028128";
#elif UNITY_IPHONE
              private string _adInterstitialAdUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
              private string _adInterstitialAdUnitId = "unused";
#endif

        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string _adRewardedUnitId = "ca-app-pub-4705307774712357/9747880899";
#elif UNITY_IPHONE
                  private string _adRewardedUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
                  private string _adRewardedUnitId = "unused";
#endif
#if UNITY_ANDROID
        private string _adRewardedInterstitialUnitId = "ca-app-pub-4705307774712357/1757416714";
#elif UNITY_IPHONE
  private string _adRewardedInterstitialUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
        private string _adRewardedInterstitialUnitId = "unused";
#endif
        public void LoadRewardedInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedInterstitialAd != null)
            {
                _rewardedInterstitialAd.Destroy();
                _rewardedInterstitialAd = null;
            }

            Debug.Log("Loading the rewarded interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");

            // send the request to load the ad.
            RewardedInterstitialAd.Load(_adRewardedInterstitialUnitId, adRequest,
                (RewardedInterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedInterstitialAd = ad;
                    RegisterEventHandlers(_rewardedInterstitialAd);
                });
        }
        public void ShowRewardedInterstitialAd(Action action)
        {
            this.Action = action;
            _hasEarnedReward = false;
            if (_rewardedInterstitialAd == null || !_rewardedInterstitialAd.CanShowAd())
            {
                //this.Action?.Invoke();
                Debug.Log("Quảng cáo chưa sẵn sàng hoặc không thể hiển thị.");
                return;
            }
            const string rewardMsg =
                "Rewarded interstitial ad rewarded the user. Type: {0}, amount: {1}.";

            if (_rewardedInterstitialAd != null && _rewardedInterstitialAd.CanShowAd())
            {
                //this.Action = action;
                _rewardedInterstitialAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    // can ok
                    _hasEarnedReward = true;
                    this.Action?.Invoke();
                    Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                });
            }
        }
        private void RegisterEventHandlers(RewardedInterstitialAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                LoadRewardedInterstitialAd();

            };
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                LoadRewardedInterstitialAd();

                Debug.LogError("Rewarded interstitial ad failed to open " +
                               "full screen content with error : " + error);
            };
        }
        /// <summary>
        /// Loads the rewarded ad.
        /// </summary>
        public void LoadRewardedAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            Debug.Log("Loading the rewarded ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            RewardedAd.Load(_adRewardedUnitId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedAd = ad;
                    RegisterEventHandlers(_rewardedAd);
                });
        }
        //private bool _hasReceivedReward = false; // Biến kiểm tra nhận thưởng
        public void ShowRewardedAd(Action action)
        {
            const string rewardMsg =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
            this.Action = action;
            _hasEarnedReward = false; // reset trước khi show ad
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                // _hasReceivedReward = false;
                //this.Action = action;
                _rewardedAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    //this.Action?.Invoke();
                    //ClawGameManager.Instance.RewardAds();
                    //this.Action = action;
                    _hasEarnedReward = true; // chỉ set khi đã xem hết
                    this.Action?.Invoke();
                    Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                });
            }
            else
            {
                Action?.Invoke();
            }
        }
        private void RegisterEventHandlers(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                //if (_hasReceivedReward)
                //{
                //    Action?.Invoke(); // Chỉ nhận thưởng nếu quảng cáo hoàn tất
                //}
                //else
                //{
                //    Debug.Log("a");
                //    //if (rewardPanel != null)
                //    //{
                //    //    rewardPanel.SetActive(false); // Tắt panel nếu chưa xem hết quảng cáo
                //    //}
                //}
                //LoadRewardedAd();
                //try
                //{
                //    PopupManager.Instance.OnUI(PopupCode.NoticeAds);
                //}
                //catch (Exception ex)
                //{
                //    Debug.LogError("Error when showing popup: " + ex.Message);
                //}
                //if (!_hasEarnedReward)
                //{
                //    PopupManager.Instance.OnUI(PopupCode.NoticeAds);
                //    Debug.Log("Rewarded interstitial ad was closed before completion. No reward given.");
                //}
                // Action?.Invoke();
                Debug.Log("Rewarded ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                LoadRewardedAd();
                Debug.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);
            };
        }
        /// <summary>
        /// Loads the interstitial ad.
        /// </summary>
        public void LoadInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            InterstitialAd.Load(_adInterstitialAdUnitId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                    RegisterEventHandlers(_interstitialAd);
                });
        }
        /// <summary>
        /// Shows the interstitial ad.
        /// </summary>
        public void ShowInterstitialAd(Action action)
        {
            this.Action = action;
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
                // action?.Invoke();
            }
        }
        private void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            // Raised when the ad is estimated to have earned money.
            interstitialAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            interstitialAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            interstitialAd.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                LoadInterstitialAd();
                this.Action?.Invoke();
                Debug.Log("Interstitial ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                LoadInterstitialAd();
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
            };
        }

        /// <summary>
        /// Creates a 320x50 banner view at top of the screen.
        /// </summary>
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (_bannerView != null)
            {
                DestroyAd();
            }

            // Create a 320x50 banner at top of the screen
            // sua vi tri .top
            _bannerView = new BannerView(_adBannerUnitId, AdSize.Banner, AdPosition.Bottom);
            ListenToAdEvents();
        }
        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void LoadAd()
        {
            // create an instance of a banner view first.
            if (_bannerView == null)
            {
                CreateBannerView();
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
        }
        /// <summary>
        /// listen to events the banner view may raise.
        /// </summary>
        private void ListenToAdEvents()
        {
            // Raised when an ad is loaded into the banner view.
            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                    + _bannerView.GetResponseInfo());
            };
            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);

                LoadAd();
            };
            // Raised when the ad is estimated to have earned money.
            _bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            _bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            _bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            _bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
                LoadAd();
            };
        }
        /// <summary>
        /// Destroys the banner view.
        /// </summary>
        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }

        public bool IsInterstitialAdReady()
        {
            return _interstitialAd != null && _interstitialAd.CanShowAd();
        }
        public bool IsRewardAdReady()
        {
            return _rewardedAd != null && _rewardedAd.CanShowAd();
        }

        public bool IsRewardedInterstitialAdReady()
        {
            return _rewardedInterstitialAd != null && _rewardedInterstitialAd.CanShowAd();
        }
    }
}
