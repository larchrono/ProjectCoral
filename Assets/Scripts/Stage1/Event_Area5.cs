using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Area5 : MonoBehaviour {

	public SceneInteractive sceneInteractive;
	public AreaClass Area_6;

	public Button GotoArea;

	public AudioSource DoorNo;
	public AudioSource DoorOpen;
	bool doorUnlock = false;

	public void UnlockGotoArea_6(){
		if (sceneInteractive.GetItemUseResult ()) {
			GotoArea.onClick.RemoveAllListeners ();
			GotoArea.onClick.AddListener (() => sceneInteractive.GoToArea (Area_6));
			doorUnlock = true;
			DoorOpen.Play ();
		} else if (doorUnlock) {
			DoorOpen.Play ();
		} else {
			DoorNo.Play ();
		}
	}
}
