using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	public static GlobalVariables instance;

	public bool isDebugArea;
	public bool isUseSound;

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

		SetUseSound(useDebugArea);
		SetUseSound(useSound);

        Debug.Log("Reading PlayerPrefs DebugArea: " + useDebugArea);
        Debug.Log("Reading PlayerPrefs UseSound: " + useSound);
	}

	public void SetDebugArea(bool src){
		isDebugArea = src;
		PlayerPrefs.SetInt("DebugArea", Convert.ToInt32(src));
	}

	public void SetUseSound(bool src){
		isUseSound = src;
		PlayerPrefs.SetInt("UseSound", Convert.ToInt32(src));
	}
}
