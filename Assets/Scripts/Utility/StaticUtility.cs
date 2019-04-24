using System;
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

	public void FadeAndAction(FadeAndActionContent content){
		if(content == null)
			return;
		float duration = content.fadeTime;
		QueueAction _action = content.actionToDo;
		GameObject _run = new GameObject("Object_FadeAndAction");
		_run.AddComponent<StaticUtility>().StartCoroutine(FadeToAction(duration,()=>{
			_action.Invoke();
			Destroy(_run,duration);
		}));
	}

	IEnumerator FadeToAction(float duration, System.Action runAction){
		BlackMask.MaskShow(duration);
		yield return new WaitForSeconds(duration);
		runAction();
		BlackMask.MaskHide(duration);
		
	}
}
