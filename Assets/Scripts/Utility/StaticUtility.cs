using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUtility : MonoBehaviour
{
	public void LogMessage(string src){
		Debug.Log (src);
	}

	public void ShowQuestionOverlay(Sprite quest){
		OverlayQuest.main.StartQuestPop(quest);
	}

	public void ShowTextOverlay(string src){
		TextOverlay.main.SetText(src);
	}

	public void SetTimeScale(float src){
		Time.timeScale = src;
	}
}
