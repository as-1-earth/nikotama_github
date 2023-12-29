using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class AdMobScript : MonoBehaviour
{
    private BannerView bannerView;
#if UNITY_ANDROID
    private string bannerUnitId = "ca-app-pub-5885349192863781/3277703363";
#elif UNITY_IPHONE
  private string bannerUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string bannerUnitId = "unused";
#endif

    private void CreateBanner()
    {
        if (bannerView != null)
        {
            DestroyBanner();
        }
        bannerView = new BannerView(bannerUnitId, AdSize.Banner, AdPosition.Bottom);
    }

    private void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }

    public void ShowBanner()
    {
        CreateBanner();

        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample education")
            .Build();

        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("ロードされました - 表示します");
            bannerView.Show();
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.Log("ロード失敗しました");
        };
        bannerView.LoadAd(adRequest);
    }
}