using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class Event_Area4 : MonoBehaviour {

	[SerializeField]
	public QueueAction OnArea4_Enter;

	[Space(10)]

	[SerializeField]
	GameObject ControlCenterDoor = null;

	bool hasGetWorkPermit;
	bool hasTouchNewspaper;
	bool hasAlertHappen;

	public AudioSource AlertLoop;
	public GameObject AlertLight;

	[Space(10)]

	[SerializeField]
	public QueueAction OnArea7_Clear;

	void Start() {
		OnArea4_Enter.Invoke();
	}

	// Update is called once per frame
	void Update () {
		if (hasGetWorkPermit && hasTouchNewspaper && !hasAlertHappen && SceneInteractive.main.inIdle) {
			hasAlertHappen = true;
			Invoke ("AreaAlert", 1.5f);
		}
	}

	public void AreaAlert(){
		SceneInteractive.main.ShowTextOverlay ("溫度過高，請立即關閉反應爐！");
		Handheld.Vibrate ();
		AlertLight.SetActive (true);
		AlertLoop.Play ();
		ControlCenterDoor.SetActive(true);
		StartCoroutine(AlertLoopSetting());
	}

	IEnumerator AlertLoopSetting(){
		yield return new WaitForSeconds(2.5f);
		float targetVolume = AlertLoop.volume/3;
		AlertLoop.DOFade(targetVolume,0.5f);
	}

	// A condition for Door open
	public void GetWorkPermit(){
		hasGetWorkPermit = true;
	}
	// A condition for Door open
	public void TouchNewsPaper(){
		hasTouchNewspaper = true;
	}

}
