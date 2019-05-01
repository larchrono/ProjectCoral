using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public GameObject[] WillTurnOn;
	public GameObject[] WillTurnOff;

	public QueueAction WhenPressA;

	public QueueAction WhenPressQ;

	// Use this for initialization
	void Start () {
		foreach (GameObject go in WillTurnOn) {
			go.SetActive (true);
		}
		foreach (GameObject go in WillTurnOff) {
			go.SetActive (false);
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.A)){
			WhenPressA.Invoke();
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			WhenPressQ.Invoke();
		}
	}

}
