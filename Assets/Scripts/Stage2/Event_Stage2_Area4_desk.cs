using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area4_desk : MonoBehaviour {

	[SerializeField]
	SceneInteractive sceneInteractive = null;
	[Space(10)]
	[SerializeField]
	GameObject Image_ExcavatorBase = null;

	[SerializeField]
	Button Button_UseBody = null;
	[SerializeField]
	Button Button_UseHand = null;
	[SerializeField]
	Button Button_GetExcavator = null;
	[SerializeField]
	Button Button_TextOverlay = null;

	[SerializeField]
	Image Image_Lobby_ExcavatorBase = null;

	[SerializeField]
	AudioSource SNDExcavatorMake = null;

	public QueueAction FinishCombine;

	public void UseExcavatorBody(){
		if (sceneInteractive.GetItemUseResult ()) {
			
			Image_ExcavatorBase.GetComponent<UsedSpritePool>().SetSpriteToPoolID(1);

			Button_UseBody.gameObject.SetActive (false);
			Button_UseHand.gameObject.SetActive (true);

			Image_Lobby_ExcavatorBase.GetComponent<UsedSpritePool>().SetSpriteToPoolID(1);

			SNDExcavatorMake.Play ();
		} else {
			sceneInteractive.ShowTextOverlay ("它好像還缺少一些零件組合起來");
		}
	}

	public void UseExcavatorHand(){
		if (sceneInteractive.GetItemUseResult ()) {

			Image_ExcavatorBase.GetComponent<UsedSpritePool>().SetSpriteToPoolID(2);

			Button_UseHand.gameObject.SetActive (false);
			//Button_GetExcavator.gameObject.SetActive (true);
			Button_TextOverlay.gameObject.SetActive (false);

			Image_Lobby_ExcavatorBase.GetComponent<UsedSpritePool>().SetSpriteToPoolID(2);

			SNDExcavatorMake.Play ();
			
			StartCoroutine(GetExcavator());

		} else {
			sceneInteractive.ShowTextOverlay ("它好像還缺少一些零件組合起來");
		}
	}

	IEnumerator GetExcavator(){
		yield return new WaitForSeconds(0.7f);
		FinishCombine.Invoke();
	}

}
