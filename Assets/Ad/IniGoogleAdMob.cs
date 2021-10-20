using GoogleMobileAds.Api;
using UnityEngine;

public class IniGoogleAdMob : MonoBehaviour
{
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }
}