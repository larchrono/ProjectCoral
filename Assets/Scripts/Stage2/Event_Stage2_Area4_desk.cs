using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area4_desk : MonoBehaviour {

	public QueueAction FinishCombine;

	public void DoGetExcavator(){
		StartCoroutine(GetExcavator());
	}

	IEnumerator GetExcavator(){
		yield return new WaitForSeconds(0.7f);
		FinishCombine.Invoke();
	}

}
