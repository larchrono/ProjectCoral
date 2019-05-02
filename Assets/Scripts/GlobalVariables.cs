using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	public static GlobalVariables instance;

	public bool isDebugArea;
	public bool isUseSound;
	public int NowStage;

	public List<int> PageItems;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}

		//Assign PlayerPrefs to Global Setting

		bool useDebugArea = PlayerPrefs.GetInt("DebugArea", 0) > 0 ? true : false;
		bool useSound = PlayerPrefs.GetInt("UseSound", 1) > 0 ? true : false;
		int useStage = PlayerPrefs.GetInt("NowStage", 1);

		SetUseSound(useDebugArea);
		SetUseSound(useSound);
		SetNowStage(useStage);

		PageItems = new List<int>();

		PageItems.Add(PlayerPrefs.GetInt("PageItem_0", 0));
		PageItems.Add(PlayerPrefs.GetInt("PageItem_1", 0));
		PageItems.Add(PlayerPrefs.GetInt("PageItem_2", 0));
		PageItems.Add(PlayerPrefs.GetInt("PageItem_3", 0));
		PageItems.Add(PlayerPrefs.GetInt("PageItem_4", 0));

        Debug.Log("Reading PlayerPrefs DebugArea: " + useDebugArea);
        Debug.Log("Reading PlayerPrefs UseSound: " + useSound);
		Debug.Log("Reading PlayerPrefs NowStage: " + NowStage);
	}

	public void SetDebugArea(bool src){
		isDebugArea = src;
		PlayerPrefs.SetInt("DebugArea", Convert.ToInt32(src));
	}

	public void SetUseSound(bool src){
		isUseSound = src;
		PlayerPrefs.SetInt("UseSound", Convert.ToInt32(src));
	}

	public void SetNowStage(int src){
		NowStage = src;
		PlayerPrefs.SetInt("NowStage", src);
		Debug.Log("Set NowStage to :" + src);
	}

	public void SetPageItemStat(int atloc, int src){
		PageItems[atloc] = src;
		PlayerPrefs.SetInt("PageItem_" + atloc, src);
		Debug.Log("Set PageItem (" + atloc + ") to :" + src);
	}
}
