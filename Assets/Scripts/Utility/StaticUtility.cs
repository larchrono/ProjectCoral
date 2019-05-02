using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUtility : MonoBehaviour
{
	public void LogMessage(string src){
		Debug.Log (src);
	}

	public void LoadSceneWithFade(string src){
		IndexMenu.main.LoadSceneWithFade(src);
	}

	public void ShowQuestionOverlay(Sprite quest){
		OverlayQuest.main.StartQuestPop(quest);
	}

	public void ShowTextOverlay(string src){
		TextOverlay.main.SetText(src);
	}

	public void ShowScrollImageOverlay(ImageOverlayClass src){
		ScrollImageOverlay.main.SetImage (src);
		ScrollImageOverlay.main.gameObject.SetActive (true);
	}

	public void ShowScrollImageOverlayFree(ImageOverlayClass src){
		ScrollImageOverlay.main.SetImageFreeSlide (src);
		ScrollImageOverlay.main.gameObject.SetActive (true);
	}

	public void ShowBookOverlay(){
		OverlayBook.main.gameObject.SetActive(true);
	}

	public void BookOverlayAddPaper(AreaPaperItemSetting paperObj){
		OverlayBook.main.AddPaper(paperObj.paperID);
		OverlayBook.main.gameObject.SetActive(true);
	}

	public void SetBGM(bool src){
		if(CrossSceneBGM.instance == null)
			return;
			
		if(src){
			CrossSceneBGM.instance.ResumeBGM();
		} else {
			CrossSceneBGM.instance.PauseBGM();
		}
	}

	public void SetTimeScale(float src){
		Time.timeScale = src;
	}

	public void BGMSwitchToMovie(){
		if(CrossSceneBGM.instance != null)
			CrossSceneBGM.instance.FadeToMovieBGM();
	}

	public void BGMSwitchToMain(){
		if(CrossSceneBGM.instance != null)
			CrossSceneBGM.instance.FadeToMainBGM();
	}

	public void PauseBGM(){
		if(CrossSceneBGM.instance != null)
			CrossSceneBGM.instance.PauseBGM();
	}

	public void ResumeBGM(){
		if(CrossSceneBGM.instance != null)
			CrossSceneBGM.instance.ResumeBGM();
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

	public void SaveGameRecordIn2(){
		GlobalVariables.instance.SetNowStage(2);
	}

	public void ClearGameRecord(){
		GlobalVariables.instance.SetNowStage(1);
		GlobalVariables.instance.SetPageItemStat(0,0);
		GlobalVariables.instance.SetPageItemStat(1,0);
		GlobalVariables.instance.SetPageItemStat(2,0);
		GlobalVariables.instance.SetPageItemStat(3,0);
		GlobalVariables.instance.SetPageItemStat(4,0);
	}
}
