using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerGoogleMobileAds : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {

        // Google AdMob Initial
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5885349192863781/3277703363"; // テスト用広告ユニットID
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-3940256099942544/2934735716"; // テスト用広告ユニットID
#else
    string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the bottom of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }
}