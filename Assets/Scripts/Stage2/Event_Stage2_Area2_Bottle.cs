using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area2_Bottle : MonoBehaviour {

	public QueueAction WhenPutFinished;

	public QueueAction AfterFinished;

	public void FinishPitting(){
		WhenPutFinished.Invoke();
		Invoke("AfterFinishPut",3f);
	}

	void AfterFinishPut(){
		Handheld.Vibrate ();
		AfterFinished.Invoke();
	}
}
