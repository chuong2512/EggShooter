using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using InitScriptName;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public enum AdType
{
	Interstitial,


}

[System.Serializable]
public class AdItem
{
	public GameState gameEvent;
	public AdType    adType;
	public int       callsTreshold;
	public int       calls;
}


public class AdsEvents : MonoBehaviour //, INonSkippableVideoAdListener
{
	public static AdsEvents THIS;
	private       string    admobUIDAndroid;
	private       string    admobUIDIOS;

	public string          nonRewardedVideoZone;
	public RewardedAdsType currentReward;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if(THIS==null)
			THIS=this;
		else if(THIS!=this)
			Destroy(gameObject);


	}


	void OnEnable() { GameEvent.OnStatus+=CheckAdsEvents; }

	public void CheckAdsEvents(GameState state)
	{
		foreach(AdItem item in LevelEditorBase.THIS.adsEvents)
		{
			if(item.gameEvent==state)
			{
				item.calls++;
				// Debug.Log(item.calls);
				if(item.calls%item.callsTreshold==0)
					ShowAdByType(item.adType);
			}

		}
	}

	void ShowAdByType(AdType adType)
	{
		if(adType==AdType.Interstitial)
			ShowAds();


	}

	public void ShowVideo()
	{
#if UNITY_ADS
		Debug.Log("show Unity ads video in " + GameEvent.Instance.GameStatus);

		if (Advertisement.IsReady("video"))
		{
			Advertisement.Show("video");
		}
		else
		{
			if (Advertisement.IsReady("defaultZone"))
			{
				Advertisement.Show("defaultZone");
			}
		}
#endif
	}


	public void ShowAds()
	{
		// Advertisements.Instance.ShowInterstitial();
	}

	private bool gameQuit;





	private readonly TimeSpan APPOPEN_TIMEOUT=TimeSpan.FromHours(4);
	private          DateTime appOpenExpireTime;
	private          float    deltaTime;
	private          bool     isShowingAppOpenAd;

	[SerializeField] private string Admob_Banner_ANDROID_ID      ="Your adMob Banner ID";
	[SerializeField] private string Admob_Interstitial_ANDROID_ID="Your adMob Interstitial ID";
	[SerializeField] private string Admob_Reward_ANDROID_ID      ="Your adMob Reward ID";

	[SerializeField] private string Admob_Banner_IOS_ID      ="Your adMob Banner ID";
	[SerializeField] private string Admob_Interstitial_IOS_ID="Your adMob Interstitial ID";
	[SerializeField] private string Admob_Reward_IOS_ID      ="Your adMob Reward ID";

	public UnityEvent OnAdLoadedEvent;
	public UnityEvent OnAdFailedToLoadEvent;
	public UnityEvent OnAdOpeningEvent;
	public UnityEvent OnAdFailedToShowEvent;
	public UnityEvent OnUserEarnedRewardEvent;
	public UnityEvent OnAdClosedEvent;
	//public bool showFpsMeter = true;
	//public Text fpsMeter;
	//public Text statusText;







}
