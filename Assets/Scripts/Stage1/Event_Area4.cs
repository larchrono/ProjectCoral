using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Area4 : MonoBehaviour {

	public SceneInteractive sceneInteractive;

	public bool HasGetWorkPermit { get; set; }
	public bool HasTouchNewspaper { get; set; }
	public bool HasAlertHappen { get; set; }

	public AudioSource AlertLoop;
	public GameObject AlertLight;

	// Update is called once per frame
	void Update () {
		if (HasGetWorkPermit && HasTouchNewspaper && !HasAlertHappen && sceneInteractive.inIdle) {
			HasAlertHappen = true;
			Invoke ("AreaAlert", 1.5f);
		}
	}

	public void AreaAlert(){
		sceneInteractive.ShowTextOverlay ("溫度過高，請立即關閉反應爐！");
		Handheld.Vibrate ();
		AlertLight.SetActive (true);
		AlertLoop.Play ();
	}

	public void GetWorkPermit(){
		HasGetWorkPermit = true;
	}

	public void AlertCondition(){
		sceneInteractive.SetConditionFlagTo (HasAlertHappen);
	}
}
