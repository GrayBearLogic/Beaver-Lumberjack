using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GoogleRewardAd
{
    public readonly RewardedAd RewardedAd;
    public GoogleRewardAd()
    {
#if UNITY_ANDROID
        const string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        const string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        const string adUnitId = "unexpected_platform";
#endif
        
        RewardedAd = new RewardedAd(adUnitId);

        RewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        RewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        RewardedAd.OnAdOpening += HandleRewardedAdOpening;
        RewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        RewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        RewardedAd.OnAdClosed += HandleRewardedAdClosed;

        var request = new AdRequest.Builder().Build();
        RewardedAd.LoadAd(request);
    }

    private void InitializeAd()
    {
        var request = new AdRequest.Builder().Build();
        RewardedAd.LoadAd(request);
    }
    public void UserChoseToWatchAd()
    {
        //InitializeAd();
        if (RewardedAd.IsLoaded())
        {
            RewardedAd.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdLoaded event received");
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.LoadAdError.GetMessage());
    }
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdOpening event received");
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.AdError.GetMessage());
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    }
}