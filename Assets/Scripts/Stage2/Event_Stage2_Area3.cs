using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area3 : MonoBehaviour {

	public GameObject ImageStampPrefabs;
	public SceneInteractive sceneInteractive;

	public AudioSource StampSound;

	public QueueAction WhenFinishStamp;
	public QueueAction WhenFinishStamp_1sec;


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

			WhenFinishStamp.Invoke();
			Invoke("DoWhenFinishStamp_1sec",1f);
		}
	}

	void DoWhenFinishStamp_1sec(){
		WhenFinishStamp_1sec.Invoke();
	}
}
