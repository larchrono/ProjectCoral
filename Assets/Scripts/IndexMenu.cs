using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class IndexMenu : MonoBehaviour {

	public static IndexMenu main;

	// Use this for initialization
	void Start () {
		main = this;
	}

	public void LoadScene(int stage){
		SceneManager.LoadScene ("Scene" + stage);
	}

	public void LoadScene(string stage){
		SceneManager.LoadScene (stage);
	}

	public void LoadSceneDelay(int stage, float delay){
		StartCoroutine (IELoadSceneDelay (stage, delay));
	}

	public void LoadSceneDelay(string stage, float delay){
		StartCoroutine (IELoadSceneDelay (stage, delay));
	}

	IEnumerator IELoadSceneDelay(int stage, float delay){
		yield return new WaitForSeconds (delay);
		LoadScene (stage);
	}

	IEnumerator IELoadSceneDelay(string stage, float delay){
		yield return new WaitForSeconds (delay);
		LoadScene (stage);
	}

	public void StartGame(){
		GameObject src = GameObject.FindWithTag("ScreenBlackMask");
		Image _blackMask = src.GetComponent<Image>();
		_blackMask.DOFade(1,2f);
		int nowStage = GlobalVariables.instance.NowStage;
		switch(nowStage)
		{
			case 1:
				LoadSceneDelay("SceneIntro",2f);
				break;

			case 2:
				LoadSceneDelay("Scene2",2f);
				break;

			default:
				LoadSceneDelay("SceneIntro",2f);
				break;
		}
	}

	public void BackToTitle(){
		GameObject src = GameObject.FindWithTag("ScreenBlackMask");
		Image _blackMask = src.GetComponent<Image>();
		_blackMask.DOFade(1,2f);
		LoadSceneDelay("Index",2f);
	}

	public void LoadSceneWithFade(string stage){
		GameObject src = GameObject.FindWithTag("ScreenBlackMask");
		Image _blackMask = src.GetComponent<Image>();
		_blackMask.DOFade(1,2f);
		LoadSceneDelay(stage,2f);
	}
}
