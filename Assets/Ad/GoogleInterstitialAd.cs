using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GoogleInterstitialAd : MonoBehaviour
{
    private InterstitialAd interstitial;

    public void InitializeAd()
    {
        string adUnitId;
//#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
        //#elif UNITY_IPHONE
        //            adUnitId = "ca-app-pub-3940256099942544/1712485313";
        //#else
        //            adUnitId = "unexpected_platform";
        //#endif
        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdLoaded += HandleinterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleinterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleinterstitialOpening;
        interstitial.OnAdFailedToShow += HandleinterstitialFailedToShow;
        interstitial.OnAdClosed += HandleinterstitialClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    public void UserChoseToWatchAd()
    {
        InitializeAd();
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void HandleinterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print(
            "HandleinterstitialLoaded event received");
    }
    public void HandleinterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleinterstitialFailedToLoad event received with message: " + args.LoadAdError.GetMessage());

    }
    public void HandleinterstitialOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print(
            "HandleinterstitialOpening event received");

    }
    public void HandleinterstitialFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleinterstitialFailedToShow event received with message: " + args.AdError.GetMessage());

    }
    public void HandleinterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleinterstitialClosed event received");

    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleinterstitialRewarded event received for " + amount.ToString() + " " + type);
    }
}