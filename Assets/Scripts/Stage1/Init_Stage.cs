using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_Stage : MonoBehaviour {

	[SerializeField]
	public QueueAction willRunAction;

	// Use this for initialization
	void Start () {
		willRunAction.Invoke ();
	}

}
