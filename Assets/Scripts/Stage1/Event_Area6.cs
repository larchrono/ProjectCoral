using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Area6 : MonoBehaviour {

	public SceneInteractive sceneInteractive;
	public AreaClass Area_8;

	public Button GoBackObject;

	public Image Clear_Light;

	public void UnlockGoBack(){
		GoBackObject.onClick.RemoveAllListeners ();
		GoBackObject.onClick.AddListener (() => sceneInteractive.GoToArea (Area_8));
		Clear_Light.gameObject.SetActive (true);
	}
}
