using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	public static GlobalVariables instance;

	//Flags
	//public bool hasAlertHappen;
	public bool isShowTouchAreaColor;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
	}

	public void SetShowTouchAreaColor(bool src){
		isShowTouchAreaColor = src;
	}
}
