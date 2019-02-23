using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Area3 : MonoBehaviour {

	public SceneInteractive sceneInteractive;

	public AudioSource KeyNo;
	public AudioSource KeyOpen;

	public void PlayKeySound(){
		bool keyResult = sceneInteractive.GetItemUseResult ();
		if (keyResult)
			KeyOpen.Play ();
		else
			KeyNo.Play ();
	}
}
