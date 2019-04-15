using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlackMask : MonoBehaviour
{
    public static Image main;
    
    void Awake()
    {
        //if it is null , will throw exception
        main = this.GetComponent<Image>();
    }

    public static void ResetMask(){
        //main.
    }

    public static void MaskShow(float duration){
        //Scene has no Fade Object , so it doesn't support fading system
        if(main == null){
            Debug.Log("Scene has no Mask Object !");
            return;
        }
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.Append(main.DOFade(0,0));
        fadeSequence.Append(main.DOFade(1,duration));
    }

    public static void MaskHide(float duration){
        //Scene has no Fade Object , so it doesn't support fading system
        if(main == null){
            Debug.Log("Scene has no Mask Object !");
            return;
        }
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.Append(main.DOFade(1,0));
        fadeSequence.Append(main.DOFade(0,duration));
    }
}
