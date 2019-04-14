﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class IndexMenu : MonoBehaviour {

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
		LoadSceneDelay("Scene1",2f);
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
