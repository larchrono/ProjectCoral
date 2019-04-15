using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class TextOverlay : MonoBehaviour {

	public static TextOverlay main;

	public event EventHandler closeOverlay;

	public Text textShow;
	public RectTransform textPanel;
	public Button ButtonBackground;

	[Space(10)]

	public float TextFlyHeight;
	public float TextFlySpeed;
	float TextBaseLoc = -100;

	//Need set Active in Scene or Set it into Init scripts
	void Awake() {
		main = this;
		gameObject.SetActive(false);
	}

	public void OnCloseOverlay(){
		if(closeOverlay != null){
			closeOverlay(this, new EventArgs());
		}
		textPanel.DOAnchorPosY(TextBaseLoc,TextFlySpeed).OnComplete(CompleteAnim);
	}

	void CompleteAnim(){
		ButtonBackground.interactable = true;
		gameObject.SetActive(false);
	}

	public void SetText(string src){
		textShow.text = src;

		textPanel.anchoredPosition = new Vector2(0,-100);
		textPanel.DOAnchorPosY(TextFlyHeight,TextFlySpeed);
	}
}
