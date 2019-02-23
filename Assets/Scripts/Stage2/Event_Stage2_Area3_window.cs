using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area3_window : MonoBehaviour {

	[SerializeField]
	Animator ImagePuzzleBackground;

	public Animator[] Puzzles;

	[SerializeField]
	int nowFocusPuzzleId = - 1;

	[Space(10)]
	[SerializeField]
	Image Image_OutdiseWindow;
	[SerializeField]
	Sprite Sprite_OutsideNewWindow;

	bool isFinish = false;

	public void ClickPuzzle(int id){
		if (isFinish)
			return;

		if (nowFocusPuzzleId != -1) {
			// Self click no handle
			if (nowFocusPuzzleId == id)
				return;

			//Change puzzle position
			ChangePuzzlePos(Puzzles[nowFocusPuzzleId].GetComponent<WindowPuzzleData>() , Puzzles[id].GetComponent<WindowPuzzleData>());
			Puzzles [nowFocusPuzzleId].SetTrigger ("Disabled");
			Puzzles [id].SetTrigger ("Disabled");

			nowFocusPuzzleId = -1;

			CheckPuzzleResult ();

		} else {
			nowFocusPuzzleId = id;
			Puzzles [nowFocusPuzzleId].Play ("Pressed");
			//Puzzles [nowFocusPuzzleId].Play ("Disabled");
		}
	}

	void CheckPuzzleResult(){
		if (Puzzles [0].GetComponent<WindowPuzzleData> ().PuzzlePosition == 0 &&
		   Puzzles [1].GetComponent<WindowPuzzleData> ().PuzzlePosition == 1 &&
		   Puzzles [2].GetComponent<WindowPuzzleData> ().PuzzlePosition == 2 &&
		   Puzzles [3].GetComponent<WindowPuzzleData> ().PuzzlePosition == 3 &&
		   Puzzles [4].GetComponent<WindowPuzzleData> ().PuzzlePosition == 4) {

			ImagePuzzleBackground.Play ("Pressed");
			isFinish = true;

			Image_OutdiseWindow.sprite = Sprite_OutsideNewWindow;
		}
	}

	void ChangePuzzlePos(WindowPuzzleData objA, WindowPuzzleData objB){
		int temppos = objB.PuzzlePosition;
		objB.PuzzlePosition = objA.PuzzlePosition;
		objA.PuzzlePosition = temppos;

		RectTransform rectA = objA.GetComponent<RectTransform> ();
		RectTransform rectB = objB.GetComponent<RectTransform> ();

		Vector2 temp = rectB.anchoredPosition;
		rectB.anchoredPosition = rectA.anchoredPosition;
		rectA.anchoredPosition = temp;
	}

}
