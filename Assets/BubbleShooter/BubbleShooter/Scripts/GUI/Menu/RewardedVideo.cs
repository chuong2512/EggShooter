using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.SceneManagement;
using InitScriptName;

using System;

public class RewardedVideo : MonoBehaviour//, IRewardedVideoAdListener
{
    public VideoWatchedEvent videoWatchedEvent;
    string rewardedVideoZone;
    public RewardedAdsType currentReward;
    // Use this for initialization
    void OnEnable()
    {


    }

    public void GetCoins(int addCoins)
    {
        RewardIcon reward = MenuManager.Instance.RewardPopup.GetComponent<RewardIcon>();
        reward.SetIconSprite(0);
        reward.gameObject.SetActive(true);
        InitScript.Instance.AddGems(addCoins);
        MenuManager.Instance.MenuCurrencyShop.GetComponent<AnimationManager>().CloseMenu();
    }

    public void GetLifes()
    {
        RewardIcon reward = MenuManager.Instance.RewardPopup.GetComponent<RewardIcon>();
        reward.SetIconSprite(1);
        reward.gameObject.SetActive(true);
        InitScript.Instance.RestoreLifes();
        MenuManager.Instance.MenuLifeShop.GetComponent<AnimationManager>().CloseMenu();

    }

    public void ContinuePlay()
    {
        MenuManager.Instance.PreFailedBanner.GetComponent<AnimationManager>().GoOnFailed();
    }

    public void ShowRewardedAds()
    {

        
        CheckRewarded();
    }

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            CheckRewarded();
        }
        else
        {
            Debug.Log("No Reward");
        }
    }

    public void CheckRewarded()
    {
        RewardIcon reward = MenuManager.Instance.RewardPopup.GetComponent<RewardIcon>();
        if (currentReward == RewardedAdsType.GetGems)
        {
            reward.SetIconSprite(0);

            reward.gameObject.SetActive(true);
            InitScript.Instance.AddGems(50);
            MenuManager.Instance.MenuCurrencyShop.GetComponent<AnimationManager>().CloseMenu();
        }
        else if (currentReward == RewardedAdsType.GetLifes)
        {
            reward.SetIconSprite(1);
            reward.gameObject.SetActive(true);
            InitScript.Instance.RestoreLifes();
            MenuManager.Instance.MenuLifeShop.GetComponent<AnimationManager>().CloseMenu();
        }
        else if (currentReward == RewardedAdsType.GetGoOn)
        {
            MenuManager.Instance.PreFailedBanner.GetComponent<AnimationManager>().GoOnFailed();
        }
    }



    //void CheckRewardedAds()
    //{
        
    //    videoWatchedEvent.Invoke(1);
 

    //}

  


    
}

[System.Serializable]
public class VideoWatchedEvent : UnityEvent<int>
{
}