using AudienceNetwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoAdsScript : MonoBehaviour
{
    [SerializeField] private Button LoadBannerBtn;
    [SerializeField] private Button LoadInterstitialBtn;
    [SerializeField] private Button ShowInterstitialBtn;

    InterstitialAd interstitialAd;
    private bool isLoaded;

    private void Awake()
    {

        AudienceNetworkAds.Initialize();
        AdSettings.AddTestDevice("5efe1787-95f0-446e-acaf-fb65bd5d6d77");
        
    }

    private void Start()
    {
        LoadBannerBtn.onClick.AddListener(LoadBanner);
        LoadInterstitialBtn.onClick.AddListener(LoadInterstitial);
        ShowInterstitialBtn.onClick.AddListener(ShowInterstitial);
    }
       
    public void LoadBanner()
    {
        AdSettings.AddTestDevice("5efe1787-95f0-446e-acaf-fb65bd5d6d77");

        AdView adView = new AdView("1030654807937989_1030655927937877", AdSize.BANNER_HEIGHT_50);
        adView.Register(this.gameObject);

        adView.AdViewDidLoad = (delegate () {
            Debug.Log("Banner loaded.");
            adView.Show(AdPosition.BOTTOM);
            LoadBannerBtn.gameObject.SetActive(false);
        });
        adView.AdViewDidFailWithError = (delegate (string error) {
            Debug.Log("Banner failed to load with error: " + error);
        });
        adView.AdViewWillLogImpression = (delegate () {
            Debug.Log("Banner logged impression.");
        });
        adView.AdViewDidClick = (delegate () {
            Debug.Log("Banner clicked.");
        });
        
        adView.LoadAd();
    }
    
    public void LoadInterstitial()
    {
        AdSettings.AddTestDevice("5efe1787-95f0-446e-acaf-fb65bd5d6d77");

        interstitialAd = new InterstitialAd("1030654807937989_1030655974604539");
        interstitialAd.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        interstitialAd.InterstitialAdDidLoad = (delegate () {
            Debug.Log("Interstitial ad loaded.");
            this.isLoaded = true;
            LoadInterstitialBtn.gameObject.SetActive(false);
            ShowInterstitialBtn.gameObject.SetActive(true);
        });
        interstitialAd.InterstitialAdDidFailWithError = (delegate (string error) {
            Debug.Log("Interstitial ad failed to load with error: " + error);
        });
        interstitialAd.InterstitialAdWillLogImpression = (delegate () {
            Debug.Log("Interstitial ad logged impression.");
        });
        interstitialAd.InterstitialAdDidClick = (delegate () {
            Debug.Log("Interstitial ad clicked.");
        });

        interstitialAd.interstitialAdDidClose = (delegate () {
            Debug.Log("Interstitial ad did close.");
            if (interstitialAd != null)
            {
                interstitialAd.Dispose();
            }
        });


        interstitialAd.LoadAd();
    }
    
    public void ShowInterstitial()
    {
        if (this.isLoaded)
        {
            this.interstitialAd.Show();
            isLoaded = false;
            LoadInterstitialBtn.gameObject.SetActive(true);
            ShowInterstitialBtn.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Interstitial Ad not loaded!");
        }
    }
}
