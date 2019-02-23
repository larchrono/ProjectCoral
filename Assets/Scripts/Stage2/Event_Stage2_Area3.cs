using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area3 : MonoBehaviour {

	public GameObject ImageStampPrefabs;
	public SceneInteractive sceneInteractive;

	public Image Background_AreaMenu;
	public Image Background_WoodFish;
	public Sprite Sprite_FinishArea;
	public Button Button_Area3WoodFish;


	bool[] Stamps = new bool[7];
	GameObject[] ImageStamps = new GameObject[7];

	Vector2 templatePos;

	bool isStampFinish = false;

	public void SetTemplatePos(RectTransform trans){
		templatePos = trans.anchoredPosition;
	}

	public void MakeStamp(int id) {
		if (!sceneInteractive.GetItemUseResult ())
			return;
		if (isStampFinish)
			return;
		if (!Stamps [id]) {
			Stamps [id] = true;
			Transform background = sceneInteractive.sceneStack.GetLastAreaStack ().transform.Find ("Background");
			ImageStamps[id] = Instantiate (ImageStampPrefabs, background);
			ImageStamps [id].GetComponent<RectTransform> ().anchoredPosition = templatePos;

		} else {
			Stamps [id] = false;
			if (ImageStamps [id] != null)
				Destroy (ImageStamps[id]);
		}
	}

	public void CheckAllStemps(){
		if (Stamps [0] && Stamps [2] && Stamps [3] && Stamps [5] && !isStampFinish) {
			isStampFinish = true;
			Background_AreaMenu.sprite = Sprite_FinishArea;
			Button_Area3WoodFish.gameObject.SetActive (true);
			Background_WoodFish.gameObject.SetActive (true);

			sceneInteractive.RemoveBagItem ("印章");
		}
	}
}
