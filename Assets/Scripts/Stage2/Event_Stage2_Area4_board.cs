﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area4_board : MonoBehaviour {


	[SerializeField]
	SceneInteractive sceneInteractive = null;

	[SerializeField]
	GameObject Prefab_ChalkCircle = null;

	[SerializeField]
	Transform Background_Q1 = null;
	[SerializeField]
	Transform Background_Q2 = null;
	[SerializeField]
	Transform nowBackground = null;

	[Space(10)]
	[SerializeField]
	GameObject Image_OutsideQuestion = null;

	[Space(10)]

	[SerializeField]
	AudioSource SNDChalkUse = null;

	bool[] CirclesQ1 = new bool[5];
	GameObject[] ImageCirclesQ1 = new GameObject[5];

	bool[] CirclesQ2 = new bool[4];
	GameObject[] ImageCirclesQ2 = new GameObject[4];

	public QueueAction FinQuestion1;
	public QueueAction FinQuestion2;

	public QueueAction BoardFinished;

	Vector2 templatePos;

	Coroutine fakeFishCoroutine;

	bool isCircleAFinish = false;
	bool isCircleBFinish = false;

	bool isDrawing = false;

	public void SetTemplatePos(RectTransform trans){
		templatePos = trans.anchoredPosition;
	}

	public void ItemCarryForMessange(){
		if(SceneInteractive.main.CheckBagHasItem("粉筆")){
			//SceneInteractive.main.ShowTextOverlay("我來回答看看");
		} else {
			SceneInteractive.main.ShowTextOverlay("好像是黑板！粉筆");
		}
	}

	IEnumerator CreateCircle(int id, int Ques){
		if (Ques == 1) {
			ImageCirclesQ1 [id] = Instantiate (Prefab_ChalkCircle, nowBackground);
			ImageCirclesQ1 [id].GetComponent<RectTransform> ().anchoredPosition = templatePos;
		} else if (Ques == 2) {
			ImageCirclesQ2 [id] = Instantiate (Prefab_ChalkCircle, nowBackground);
			ImageCirclesQ2 [id].GetComponent<RectTransform> ().anchoredPosition = templatePos;
		}
		isDrawing = true;
		yield return new WaitForSeconds (0.5f);
		isDrawing = false;

		if (Ques == 1)
			StartCoroutine (CheckAllCircleQ1 ());
		if (Ques == 2)
			StartCoroutine (CheckAllCircleQ2 ());
	}

	IEnumerator ClearCircle(int id, int Ques){
		if (Ques == 1) {
			if (ImageCirclesQ1 [id] != null)
				Destroy (ImageCirclesQ1[id]);
		} else if (Ques == 2) {
			if (ImageCirclesQ2 [id] != null)
				Destroy (ImageCirclesQ2[id]);
		}

		isDrawing = true;
		yield return new WaitForSeconds (0.5f);
		isDrawing = false;

		if (Ques == 1)
			StartCoroutine (CheckAllCircleQ1 ());
		if (Ques == 2)
			StartCoroutine (CheckAllCircleQ2 ());
	}

	public void MakeCircle_Q1(int id) {
		if (isDrawing)
			return;
		if (!CirclesQ1 [id]) {
			CirclesQ1 [id] = true;
			SNDChalkUse.Play ();
			StartCoroutine (CreateCircle (id, 1));

			if(fakeFishCoroutine != null)
				StopCoroutine(fakeFishCoroutine);
				
		} else {
			CirclesQ1 [id] = false;
			StartCoroutine (ClearCircle (id, 1));
		}
	}

	public void MakeCircle_Q2(int id) {
		if (isCircleBFinish)
			return;
		if (isDrawing)
			return;
		
		if (!CirclesQ2 [id]) {
			CirclesQ2 [id] = true;
			SNDChalkUse.Play ();
			StartCoroutine (CreateCircle (id, 2));

		} else {
			CirclesQ2 [id] = false;
			StartCoroutine (ClearCircle (id, 2));
		}
	}

	IEnumerator CheckAllCircleQ1(){
		if (CirclesQ1 [0] && !CirclesQ1 [1] && !CirclesQ1 [2] && CirclesQ1 [3] && !CirclesQ1 [4] && !isCircleAFinish) {
			isCircleAFinish = true;

			FinQuestion1.Invoke();

			isDrawing = true;
			yield return new WaitForSeconds (1.0f);
			isDrawing = false;

			Background_Q1.gameObject.SetActive (false);
			Background_Q2.gameObject.SetActive (true);

			//Image_OutsideSecondBoard.SetActive (false);
			Image_OutsideQuestion.GetComponent<UsedSpritePool>().SetSpriteToPoolID(1);

			nowBackground = Background_Q2;
		}
	}

	IEnumerator CheckAllCircleQ2(){
		if (CirclesQ2 [0] && CirclesQ2 [1] && CirclesQ2 [2] && CirclesQ2 [3] && !isCircleBFinish) {
			isCircleBFinish = true;
			
			FinQuestion2.Invoke();

			isDrawing = true;
			yield return new WaitForSeconds (1.0f);
			isDrawing = false;

			BoardFinished.Invoke();

			//sceneInteractive.DestroyNowFocusBagItem ();
			//sceneInteractive.RemoveBagItem("粉筆");

		}
	}

	public void FakeFishClick(){
		fakeFishCoroutine =  StartCoroutine(DelayFakeFishClick());
	}

	IEnumerator DelayFakeFishClick(){
		yield return new WaitForSeconds(2.0f);
		SceneInteractive.main.ShowTextOverlay("再看近一點");
	}

}
