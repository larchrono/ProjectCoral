using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area4_desk : MonoBehaviour {

	[SerializeField]
	SceneInteractive sceneInteractive;
	[Space(10)]
	[SerializeField]
	GameObject Image_ExcavatorBase;
	[SerializeField]
	GameObject Image_ExcavatorBody;
	[SerializeField]
	GameObject Image_ExcavatorFull;
	[SerializeField]
	Button Button_UseBody;
	[SerializeField]
	Button Button_UseHand;
	[SerializeField]
	Button Button_GetExcavator;
	[SerializeField]
	Button Button_TextOverlay;

	public void UseExcavatorBody(){
		if (sceneInteractive.GetItemUseResult ()) {

			Image_ExcavatorBase.gameObject.SetActive (false);
			Image_ExcavatorBody.gameObject.SetActive (true);

			Button_UseBody.gameObject.SetActive (false);
			Button_UseHand.gameObject.SetActive (true);
		}
	}

	public void UseExcavatorHand(){
		if (sceneInteractive.GetItemUseResult ()) {

			Image_ExcavatorBody.gameObject.SetActive (false);
			Image_ExcavatorFull.gameObject.SetActive (true);

			Button_UseHand.gameObject.SetActive (false);
			Button_GetExcavator.gameObject.SetActive (true);
			Button_TextOverlay.gameObject.SetActive (false);
		}
	}

}
