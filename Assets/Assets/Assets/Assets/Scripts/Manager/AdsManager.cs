using System;
using System.Collections;
using System.Collections.Generic;
using Cashbaazi.App.Helper;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Cashbaazi.App.Manager
{
    public class AdsManager : Singleton<AdsManager>
    {
        [SerializeField] string KEY_Banner;
        [SerializeField] string KEY_Rewarded;
        [SerializeField] bool isTesting;

        private BannerView bannerView;
        private RewardedAd rewardedAd;

        Action rewardCallback;
        bool rewardEarned;

        private void Start()
        {
            MobileAds.Initialize(initStatus =>
            {
                Debug.Log("Admob init done");
                Reqeust_Rewarded();
            });
        }



        public void Show_Banner()
        {
            this.bannerView = new BannerView(isTesting ? "ca-app-pub-3940256099942544/6300978111" : KEY_Banner, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerView.LoadAd(request);
        }
        public void Hide_Banner()
        {
            if (this.bannerView == null)
                return;

            this.bannerView.Destroy();
        }



        public void Show_Rewarded(Action callback)
        {
            rewardEarned = false;
            rewardCallback = callback;
        }
        private void Reqeust_Rewarded()
        {
            this.rewardedAd = new RewardedAd(isTesting ? "ca-app-pub-3940256099942544/5224354917" : KEY_Rewarded);

            this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            this.rewardedAd.LoadAd(request);
        }

        private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Reqeust_Rewarded();
        }

        private void HandleUserEarnedReward(object sender, Reward e)
        {
            rewardEarned = true;
        }

        private void HandleRewardedAdClosed(object sender, EventArgs e)
        {
            if(rewardEarned && rewardCallback != null)
            {
                rewardCallback();
                rewardCallback = null;
            }

            Reqeust_Rewarded();
        }
    }
}