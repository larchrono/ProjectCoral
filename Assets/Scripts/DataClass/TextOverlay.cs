using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextOverlay : MonoBehaviour {

	public static TextOverlay main;
	public event EventHandler OverlayFinish;

	public Text textShow;
	public RectTransform textPanel;

	[Space(10)]

	public float normal_YPOS;
    public float pop_YPOS;
    public float TimeFly;
    public float TimeStay;

	Sequence seqAction;

	//Need set Active in Scene or Set it into Init scripts
	void Awake() {
		main = this;
		gameObject.SetActive(false);
	}

	void OnOverlayFinish(){
		if(OverlayFinish != null){
			OverlayFinish(this, new EventArgs());
		}
	}

	public void StartTextPop(){
		if(seqAction != null){
			if(seqAction.IsComplete()){
				seqAction.Restart();
			} else {
				seqAction.Goto(TimeFly,true);
			}
		} else {
        	seqAction = DOTween.Sequence()
            .OnStart(()=>{
                textPanel.anchoredPosition = new Vector2(0,normal_YPOS);
            })
            .Append(textPanel.DOAnchorPosY(pop_YPOS,TimeFly))
            .AppendInterval(TimeStay)
            .Append(textPanel.DOAnchorPosY(normal_YPOS,TimeFly))
            .OnComplete(OnOverlayFinish)
			.SetAutoKill(false);

			seqAction.Play();
		}
    }

	public void SetText(string src){
		textShow.text = src;
		StartTextPop();
		gameObject.SetActive(true);
	}
}
