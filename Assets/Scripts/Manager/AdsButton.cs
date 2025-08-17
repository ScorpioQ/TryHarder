using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
#if UNITY_IOS
    private string gameID = "4256634";
#elif UNITY_ANDROID
    private string gameID = "4256635";
#endif

    Button adsButton;
    public string placementID = "Rewarded_Android";

    void Start()
    {
        adsButton = GetComponent<Button>();
        //adsButton.interactable = Advertisement.IsReady(placementID);

        if (adsButton)
            adsButton.onClick.AddListener(ShowRewardAds);

        //Advertisement.AddListener(this);
        //Advertisement.Initialize(gameID, true);
    }

    public void ShowRewardAds()
    {
        Advertisement.Show(placementID);
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Finished:
                Debug.Log("ads success");
                FindObjectOfType<PlayerController>().health = 3;
                FindObjectOfType<PlayerController>().isDead = false;
                UIManager.instance.UpdateHealth(3);
                break;
            case ShowResult.Skipped:
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {/*
        if (Advertisement.IsReady(placementID))
        {
            Debug.Log("广告准备好了");
        }*/
    }
}
