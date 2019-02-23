using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area4_board : MonoBehaviour {

	[SerializeField]
	SceneInteractive sceneInteractive;

	[SerializeField]
	GameObject Prefab_ChalkCircle;

	[SerializeField]
	Transform Background_Q1;
	[SerializeField]
	Transform Background_Q2;
	[SerializeField]
	Transform nowBackground;
	[Space(10)]
	[SerializeField]
	GameObject Image_OutsideQ1;
	[SerializeField]
	GameObject Image_OutsideQ2;
	[Space(10)]
	[SerializeField]
	Image Image_Background;
	[SerializeField]
	Sprite Sprite_BoardFall;
	[SerializeField]
	Sprite Sprite_BoardCrack;

	[SerializeField]
	Button Button_Crack;

	bool[] CirclesQ1 = new bool[5];
	GameObject[] ImageCirclesQ1 = new GameObject[5];

	bool[] CirclesQ2 = new bool[4];
	GameObject[] ImageCirclesQ2 = new GameObject[4];

	Vector2 templatePos;


	bool isCircleAFinish = false;
	bool isCircleBFinish = false;

	public void SetTemplatePos(RectTransform trans){
		templatePos = trans.anchoredPosition;
	}

	public void MakeCircle_Q1(int id) {
		if (!sceneInteractive.GetItemUseResult ())
			return;
		if (!CirclesQ1 [id]) {
			CirclesQ1 [id] = true;
			ImageCirclesQ1[id] = Instantiate (Prefab_ChalkCircle, nowBackground);
			ImageCirclesQ1 [id].GetComponent<RectTransform> ().anchoredPosition = templatePos;

		} else {
			CirclesQ1 [id] = false;
			if (ImageCirclesQ1 [id] != null)
				Destroy (ImageCirclesQ1[id]);
		}

		CheckAllCircleQ1 ();
	}

	public void MakeCircle_Q2(int id) {
		if (!sceneInteractive.GetItemUseResult ())
			return;
		if (isCircleBFinish)
			return;
		
		if (!CirclesQ2 [id]) {
			CirclesQ2 [id] = true;
			ImageCirclesQ2[id] = Instantiate (Prefab_ChalkCircle, nowBackground);
			ImageCirclesQ2 [id].GetComponent<RectTransform> ().anchoredPosition = templatePos;

		} else {
			CirclesQ2 [id] = false;
			if (ImageCirclesQ2 [id] != null)
				Destroy (ImageCirclesQ2[id]);
		}

		CheckAllCircleQ2 ();
	}

	public void CheckAllCircleQ1(){
		if (CirclesQ1 [0] && !CirclesQ1 [1] && !CirclesQ1 [2] && CirclesQ1 [3] && !CirclesQ1 [4] && !isCircleAFinish) {
			isCircleAFinish = true;

			Background_Q1.gameObject.SetActive (false);
			Background_Q2.gameObject.SetActive (true);

			Image_OutsideQ1.SetActive (false);
			Image_OutsideQ2.SetActive (true);

			nowBackground = Background_Q2;
		}
	}

	public void CheckAllCircleQ2(){
		if (CirclesQ2 [0] && CirclesQ2 [1] && CirclesQ2 [2] && CirclesQ2 [3] && !isCircleBFinish) {
			isCircleBFinish = true;

			Image_Background.sprite = Sprite_BoardFall;
			Button_Crack.gameObject.SetActive (true);

			sceneInteractive.DestroyNowFocusBagItem ();

		}
	}

}
