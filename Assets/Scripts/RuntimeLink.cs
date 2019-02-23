using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeLink : MonoBehaviour {

	public string TargetObjectName;

	public void RuntimeFocusBagItem(BagItem src){
		SceneInteractive sceneInteractive = GameObject.Find ("SceneInteractive").GetComponent<SceneInteractive> ();
		sceneInteractive.FocusBagItem (src);
	}

	public void RuntimeGetItem(BagItem item){
		SceneInteractive sceneInteractive = GameObject.Find ("SceneInteractive").GetComponent<SceneInteractive> ();
		sceneInteractive.GetItem (item);
	}

	public void RuntimeFlagSet(string flag){
		//GlobalVariables globalVariables = GameObject.Find ("GlobalData").GetComponent<GlobalVariables> ();
		//globalVariables.FlagSet (flag);
	}

	public void RuntimeShowTextOverlay(string src){
		SceneInteractive sceneInteractive = GameObject.Find ("SceneInteractive").GetComponent<SceneInteractive> ();
		sceneInteractive.ShowTextOverlay (src);
	}

	public void RuntimeDisableGameObject(string ObjectName){
		GameObject dist = GameObject.Find (ObjectName);
		if (dist != null)
			dist.SetActive (false);
	}

	public void FocusGameObject(string ObjectName){
		TargetObjectName = ObjectName;
	}

	public void SendMessageToGameObject(string methodName){
		GameObject dist = GameObject.Find (TargetObjectName);
		if (dist != null)
			dist.BroadcastMessage (methodName,SendMessageOptions.DontRequireReceiver);
	}
}
