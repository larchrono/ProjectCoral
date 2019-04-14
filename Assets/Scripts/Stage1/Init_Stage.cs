using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_Stage : MonoBehaviour {

	[Header("Auto Setting Sound via GlobalVariable")]

	[Space(10)]

	[SerializeField]
	public QueueAction willRunAction;

	[SerializeField]
	GameObject [] NeedInitOverlays;

	// Use this for initialization
	void Start () {
		SettingSoundOption();

		willRunAction.Invoke ();

		foreach (var item in NeedInitOverlays)
		{
			item.SetActive(true);
			item.SetActive(false);
		}
	}

	public void SettingSoundOption(){
		bool _soundSetting = GlobalVariables.instance.isUseSound;
		Camera.main.GetComponent<AudioListener>().enabled = _soundSetting;
	}

}
