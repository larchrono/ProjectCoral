using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public GameObject[] WillTurnOn;
	public GameObject[] WillTurnOff;

	// Use this for initialization
	void Start () {
		foreach (GameObject go in WillTurnOn) {
			go.SetActive (true);
		}
		foreach (GameObject go in WillTurnOff) {
			go.SetActive (false);
		}
	}

}
