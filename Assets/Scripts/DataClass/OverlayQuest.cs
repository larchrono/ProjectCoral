using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OverlayQuest : MonoBehaviour
{
    public static OverlayQuest main;
    public event EventHandler OverlayFinish;

    public RectTransform Image_Quest;
    public float normal_YPOS;
    public float pop_YPOS;
    public float TimeFly;
    public float TimeStay;

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

    public void StartQuestPop(Sprite src){
        Image_Quest.GetComponent<Image>().sprite = src;
        Sequence seq = DOTween.Sequence()
            .OnStart(()=>{
                Image_Quest.anchoredPosition = new Vector2(0,normal_YPOS);
            })
            .Append(Image_Quest.DOAnchorPosY(pop_YPOS,TimeFly))
            .AppendInterval(TimeStay)
            .Append(Image_Quest.DOAnchorPosY(normal_YPOS,TimeFly))
            .OnComplete(OnOverlayFinish);

        gameObject.SetActive(true);
        seq.Play();
    }
}
