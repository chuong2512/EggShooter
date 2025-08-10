﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using InitScriptName;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
	public bool PlayOnEnable=true;
	bool        WaitForPickupFriends;

	bool                                                 WaitForAksFriends;
	System.Collections.Generic.Dictionary<string,string> parameters;

	void OnEnable()
	{
		if(PlayOnEnable)
		{
			if(SoundBase.Instance!=null)
				SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.swish[0]);

			//if( !GetComponent<SequencePlayer>().sequenceArray[0].isPlaying )
			//    GetComponent<SequencePlayer>().Play();
			if(name=="Fire")
			{


			}
		}
		//if (name == "MenuPlay")
		//{
		//    for (int i = 1; i <= 3; i++)
		//    {
		//        transform.Find("Image").Find("Star" + i).gameObject.SetActive(false);
		//    }
		//    int stars = PlayerPrefs.GetInt(string.Format("Level.{0:000}.StarsCount", PlayerPrefs.GetInt("OpenLevel")), 0);
		//    if (stars > 0)
		//    {
		//        for (int i = 1; i <= stars; i++)
		//        {
		//            transform.Find("Image").Find("Star" + i).gameObject.SetActive(true);
		//        }

		//    }
		//    else
		//    {
		//        for (int i = 1; i <= 3; i++)
		//        {
		//            transform.Find("Image").Find("Star" + i).gameObject.SetActive(false);
		//        }

		//    }

		//}

		//		if (name == "PreFailedBanner") {
		//			transform.Find ("Image/Video").gameObject.SetActive (false);
		//		}

		if(transform.Find("Image/Video")!=null)
		{
#if UNITY_ADS
			InitScript.Instance.rewardedVideoZone="rewardedVideo";

			//            if (!LevelEditorBase.THIS.enableUnityAds || !AdsEvents.THIS.GetRewardedUnityAdsReady())
			//                transform.Find("Image/Video").gameObject.SetActive(false);
#else
			//			transform.Find ("Image/Video").gameObject.SetActive (false);
#endif
		}

	}

	//	void OnDisable () {
	//		if (transform.Find ("Image/Video") != null) {
	//			transform.Find ("Image/Video").gameObject.SetActive (true);
	//		}
	//
	//
	//		//if( PlayOnEnable )
	//		//{
	//		//    if( !GetComponent<SequencePlayer>().sequenceArray[0].isPlaying )
	//		//        GetComponent<SequencePlayer>().sequenceArray[0].Play
	//		//}
	//	}

	public void OnFinished()
	{
		if(GetComponent<MenuComplete>()!=null)
		{
			GetComponent<MenuComplete>().OnAnimationFinished();
		}

		if(GetComponent<MenuPlay>()!=null)
		{
			GetComponent<MenuPlay>().OnAnimationFinished();
		}
		if(name=="MenuPlay")
		{
			//            InitScript.Instance.currentTarget = InitScript.Instance.targets[PlayerPrefs.GetInt("OpenLevel")];
			//GetComponent<MenuPlay>().OnAnimationFinished();

		}
		if(name=="PreFailedBanner")
		{
			//			transform.Find ("Image/Video").gameObject.SetActive (false);
			//			if (LevelEditorBase.THIS.enableUnityAds) {
			//
			//				if (AdsEvents.THIS.GetRewardedUnityAdsReady ()) {
			//					transform.Find ("Image/Video").gameObject.SetActive (true);
			//				}
			//			}
		}
#if UNITY_ADS
#endif

	}

	public void PlaySoundButton() { SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click); }

	public IEnumerator Close() { yield return new WaitForSeconds(0.5f); }

	public void CloseMenu()
	{

		if(transform.parent.GetComponent<MenuManager>()!=null)
			transform.parent.GetComponent<MenuManager>().OnCloseMenuEvent();
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(gameObject.name=="PreFailedBanner")
			GameEvent.Instance.GameStatus=GameState.GameOver;
		if(gameObject.name=="MenuComplete")
		{
			GoToMap();
		}
		if(gameObject.name=="MenuGameOver")
		{
			GoToMap();
		}

		if(SceneManager.GetActiveScene()==SceneManager.GetSceneByName("game"))
		{
			if(GameEvent.Instance.GameStatus==GameState.Pause)
			{
				GameEvent.Instance.GameStatus=GameState.WaitAfterClose;

			}
		}
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.swish[1]);

		gameObject.SetActive(false);
	}

	public void Pause() { GameEvent.Instance.GameStatus=GameState.Pause; }

	public void ShowMenu(GameObject menu) { menu.SetActive(true); }

	public void GoToMap()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(MenuManager.Instance!=null)
			ShowLoading(MenuManager.Instance.Loading);
		SceneManager.LoadScene("map");
	}

	public void GoToMenu()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);

		SceneManager.LoadScene("menu");
	}

	public void QuitGame() { Application.Quit(); }



	public void Rateus()
	{
		string ANDROID_STORE_URL="market://details?id="+Application.identifier;
		string IOS_STORE_URL    ="itms-apps://itunes.apple.com/app/id"+Application.identifier;
		string WINDOWS_STORE_URL="ms-windows-store://review/?ProductId="+Application.identifier;

		string storeURL="";

#if UNITY_ANDROID
		storeURL=ANDROID_STORE_URL;
#elif UNITY_IOS
        storeURL=IOS_STORE_URL;
#elif UNITY_WSA
        storeURL=WINDOWS_STORE_URL;
#endif

		Application.OpenURL(storeURL);
	}

	public void ShowLoading(GameObject loading) { loading.SetActive(true); }

	public void LoseLifeAndGoMap()
	{
		InitScript.Instance.SpendLife(1);
		GoToMap();
	}

	public void Play()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(gameObject.name=="PreFailedBanner")
		{
			if(InitScript.Gems>=LevelEditorBase.THIS.FailedCost)
			{
				InitScript.Instance.SpendGems(LevelEditorBase.THIS.FailedCost);
				LevelData.LimitAmount        +=LevelEditorBase.THIS.ExtraFailedMoves;
				GameEvent.Instance.GameStatus= GameState.WaitAfterClose;
				gameObject.SetActive(false);

			}
			else
			{
				BuyGems();
			}
		}
		else if(gameObject.name=="MenuGameOver")
		{
			GoToMap();
		}
		else if(gameObject.name=="MenuPlay")
		{
			if(InitScript.Lifes>0)
			{

				if(MenuManager.Instance!=null)
					ShowLoading(MenuManager.Instance.Loading);
				SceneManager.LoadScene("game");
			}
			else
			{
				BuyLifeShop();
			}

		}
		else if(gameObject.name=="PlayMain")
		{
			GoToMap();
		}
	}

	public void RestartLevel() //1.2
	{
		if(InitScript.Lifes>0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			BuyLifeShop();
		}

	}
	public void PlayTutorial()
	{
		//        SoundBase.Instance.audio.PlayOneShot( SoundBase.Instance.click );
		GameEvent.Instance.GameStatus=GameState.Playing;
		//    mainscript.Instance.dropDownTime = Time.time + 0.5f;
		//        CloseMenu();
	}

	public void GoOnFailed()
	{
		LevelData.LimitAmount        +=5;
		GameEvent.Instance.GameStatus= GameState.WaitAfterClose;
		gameObject.SetActive(false);
	}

	public void Next()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		CloseMenu();
	}

	public void BuyGems()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		MenuManager.Instance.ShowCurrencyShop();
	}

	public void Buy(GameObject pack)
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.coins);
		if(pack.name=="Pack1")
		{
			InitScript.waitedPurchaseGems=10;
			IAPManager.OnPurchaseSuccess=() =>
			{
				InitScript.Instance.PurchaseSucceded();
				CloseMenu();
			};
			
			IAPManager.Instance.BuyProductID(IAPKey.PACK1);
		}

		if(pack.name=="Pack2")
		{
			InitScript.waitedPurchaseGems=20;
			IAPManager.OnPurchaseSuccess=() =>
			{
				InitScript.Instance.PurchaseSucceded();
				CloseMenu();
			};
			
			IAPManager.Instance.BuyProductID(IAPKey.PACK2);
		}
		if(pack.name=="Pack3")
		{
			InitScript.waitedPurchaseGems=30;
			IAPManager.OnPurchaseSuccess=() =>
			{
				InitScript.Instance.PurchaseSucceded();
				CloseMenu();
			};
			
			IAPManager.Instance.BuyProductID(IAPKey.PACK3);
		}
		if(pack.name=="Pack4")
		{
			InitScript.waitedPurchaseGems=50;
			IAPManager.OnPurchaseSuccess=() =>
			{
				InitScript.Instance.PurchaseSucceded();
				CloseMenu();
			};
			
			IAPManager.Instance.BuyProductID(IAPKey.PACK4);
		}
		if(pack.name=="Pack5")
		{
			InitScript.waitedPurchaseGems=100;
			IAPManager.OnPurchaseSuccess=() =>
			{
				InitScript.Instance.PurchaseSucceded();
				CloseMenu();
			};
			
			IAPManager.Instance.BuyProductID(IAPKey.PACK5);
		}
		CloseMenu();

	}

	public void BuyLifeShop()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(InitScript.Lifes<InitScript.CapOfLife)
			MenuManager.Instance.ShowLifeShop();

	}

	public void BuyLife(GameObject button)
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(InitScript.Gems>=int.Parse(button.transform.Find("PriceRefill").GetComponent<Text>().text))
		{
			//1.1
			InitScript.Instance.SpendGems(int.Parse(button.transform.Find("PriceRefill").GetComponent<Text>().text)); //1.1
			InitScript.Instance.RestoreLifes();
			CloseMenu();
		}
		else
		{
			MenuManager.Instance.ShowCurrencyShop();
		}

	}

	public void BuyBubbles()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(InitScript.Gems>=50)
		{
			//1.1
			InitScript.Instance.SpendGems(50); //1.1
			GoOnFailed();
			CloseMenu();
		}
		else
		{
			MenuManager.Instance.ShowCurrencyShop();
		}

	}

	public void ShowAds()
	{
		//		if (name == "GemsShop")
		//			AdsEvents.THIS.currentReward = RewardedAdsType.GetGems;
		//		else if (name == "LiveShop")
		//			AdsEvents.THIS.currentReward = RewardedAdsType.GetLifes;
		//		else if (name == "PreFailedBanner")
		//			AdsEvents.THIS.currentReward = RewardedAdsType.GetGoOn;
		//		AdsEvents.THIS.ShowRewardedAds ();
		if(name=="PreFailedBanner")
			return;
		CloseMenu();
	}


	void ShowGameOver()
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.gameOver);

		GameObject.Find("Canvas").transform.Find("MenuGameOver").gameObject.SetActive(true);
		gameObject.SetActive(false);

	}

	#region Settings

	public void ShowSettings(GameObject menuSettings)
	{
		SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);
		if(!menuSettings.activeSelf)
		{
			menuSettings.SetActive(true);
			//           menuSettings.GetComponent<SequencePlayer>().Play();
		}
		else
			menuSettings.SetActive(false);
	}

	public void SoundOff(GameObject Off)
	{
		if(!Off.activeSelf)
		{
			SoundBase.Instance.GetComponent<AudioSource>().volume=0;
			InitScript.sound                                     =false;

			Off.SetActive(true);
		}
		else
		{
			SoundBase.Instance.GetComponent<AudioSource>().volume=1;
			InitScript.sound                                     =true;

			Off.SetActive(false);

		}
		PlayerPrefs.SetInt("Sound",(int) SoundBase.Instance.GetComponent<AudioSource>().volume);
		PlayerPrefs.Save();
		if(PlayerPrefs.GetInt("Sound")==0)
			SoundBase.Instance.mixer.SetFloat("soundVolume",-80);
		else
			SoundBase.Instance.mixer.SetFloat("soundVolume",1);
	}

	public void MusicOff(GameObject Off)
	{
		if(!Off.activeSelf)
		{
			GameObject.Find("Music").GetComponent<AudioSource>().volume=0;
			InitScript.music                                           =false;

			Off.SetActive(true);
		}
		else
		{
			GameObject.Find("Music").GetComponent<AudioSource>().volume=1f;
			InitScript.music                                           =true;

			Off.SetActive(false);

		}
		PlayerPrefs.SetFloat("Music",GameObject.Find("Music").GetComponent<AudioSource>().volume);
		PlayerPrefs.Save();

	}

	public void Info()
	{
		//		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("map") || SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("menu"))
		//			GameObject.Find ("Canvas").transform.Find ("Tutorial").gameObject.SetActive (true);
		//		else
		//			GameObject.Find ("Canvas").transform.Find ("PreTutorial").gameObject.SetActive (true);
	}

	public void Quit()
	{
		if(SceneManager.GetActiveScene()==SceneManager.GetSceneByName("game"))
		{
			InitScript.Instance.AddLife(1);
			GoToMap();
		}
		else
			Application.Quit();
	}

	#endregion



}
