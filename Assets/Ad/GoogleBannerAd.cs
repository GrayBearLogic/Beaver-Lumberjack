using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GoogleBannerAd : MonoBehaviour
{
    private BannerView bannerView;
    public UnityEvent<GameObject> OnAdLoaded;
    public UnityEvent<GameObject> OnAdFailedToLoad;
    public UnityEvent<GameObject> OnAdOpening;
    public UnityEvent<GameObject> OnAdClosed;
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
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        bannerView.OnAdLoaded += HandleRewardedAdLoaded;
        bannerView.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        bannerView.OnAdOpening += HandleRewardedAdOpening;
        bannerView.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }
    public void UserChoseToWatchAd()
    {
        InitializeAd();
        bannerView.Show();       
    }

    #region events
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdLoaded event received");
        OnAdLoaded.Invoke(sender as GameObject);
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.LoadAdError.GetMessage());
        OnAdFailedToLoad.Invoke(sender as GameObject);

    }
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdOpening event received");
        OnAdOpening.Invoke(sender as GameObject);

    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        OnAdClosed.Invoke(sender as GameObject);

    }
    #endregion
}