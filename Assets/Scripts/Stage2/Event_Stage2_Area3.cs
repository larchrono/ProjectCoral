using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area3 : MonoBehaviour {

	public GameObject ImageStampPrefabs;
	public SceneInteractive sceneInteractive;

	public Image Background_AreaMenu;
	public Image Background_WoodFish;
	public Button Button_Area3WoodFish;

	public AudioSource StampSound;
	public AudioSource MenuFinished;


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

			StampSound.Play ();

			CheckAllStemps ();

		} else {
			Stamps [id] = false;
			if (ImageStamps [id] != null)
				Destroy (ImageStamps[id]);

			CheckAllStemps ();
		}
	}

	public void CheckAllStemps(){
		if (Stamps [0] && !Stamps [1] && Stamps [2] && Stamps [3] && !Stamps [4] && Stamps [5] && !Stamps [6] && !isStampFinish) {
			isStampFinish = true;
			Background_AreaMenu.GetComponent<UsedSpritePool>().SetSpriteToPoolID(1);
			Button_Area3WoodFish.gameObject.SetActive (true);
			Background_WoodFish.gameObject.SetActive (true);

			sceneInteractive.RemoveBagItem ("印章");
			MenuFinished.Play ();
		}
	}
}
