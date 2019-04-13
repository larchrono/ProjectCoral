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
