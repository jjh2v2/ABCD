using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    public string android_banner_id = "ca-app-pub-4088065019947684/3271647590";
    public string android_interstitial_id = "ca-app-pub-4088065019947684/7566115828";
    public string android_reward_id = "ca-app-pub-4088065019947684/8496054111";

    public string ios_banner_id = "ca-app-pub-4088065019947684/5451212086";
    public string ios_interstitial_id = "ca-app-pub-4088065019947684/1675618608";
    public string ios_reward_id= "ca-app-pub-4088065019947684/9362536938";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private Reward reward;

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-4088065019947684~2589121315";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif

        MobileAds.SetiOSAppPauseOnBackground(true);
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        RequestBannerAd();
        RequestInterstitialAd();

        ShowBannerAd();
    }

    public void RequestBannerAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_banner_id;
#elif UNITY_IOS
        adUnitId = ios_bannerAdUnitId;
#endif

        //MobileAds.Initialize(adUnitId);

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        //// Called when an ad request has successfully loaded.
        //bannerView.OnAdLoaded += HandleOnAdLoaded;
        //// Called when an ad request failed to load.
        //bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //// Called when an ad is clicked.
        //bannerView.OnAdOpening += HandleOnAdOpened;
        //// Called when the user returned from the app after an ad click.
        //bannerView.OnAdClosed += HandleOnAdClosed;
        //// Called when the ad click caused the user to leave the application.
        //bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        Invoke("SpawnBannerView", 5);
    }

    void SpawnBannerView()
    {
        bannerView.Show();
    }

    /*
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLoaded event received");
        Debug.Log("HandleOnAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
        //                    + args.Message);
        Debug.Log("HandleOnAdFailedToLoad event received with message: "
                            + args.Message);
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdOpened event received");
        Debug.Log("HandleOnAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdClosed event received");
        Debug.Log("HandleOnAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLeavingApplication event received");
        Debug.Log("HandleOnAdLeavingApplication event received");
    }
    */
    private void RequestInterstitialAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_interstitial_id;
#elif UNITY_IOS
        adUnitId = ios_interstitialAdUnitId;
#endif

        interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        interstitialAd.LoadAd(request);

        interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received.");

        interstitialAd.Destroy();

        RequestInterstitialAd();
    }

    public void ShowBannerAd()
    {
        bannerView.Show();
    }

    public void ShowInterstitialAd()
    {
        if (!interstitialAd.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }

        interstitialAd.Show();
    }

}