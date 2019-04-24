using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelPlayerBag : MonoBehaviour
{
    [Header("This plugin contain Animation for show/hide")]
    [Space(10)]
    public float HalfWidth;
    public float normal_XPOS;
    public float pop_XPOS;

    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        normal_XPOS = rectTransform.rect.width;

        SceneInteractive.main.DebugMsg(""+Camera.main.aspect);
        if (Camera.main.aspect >= 2.1){
            SceneInteractive.main.DebugMsg("19:9");
        } 
        else if (Camera.main.aspect >= 1.7){
            SceneInteractive.main.DebugMsg("16:9");
            pop_XPOS = normal_XPOS - HalfWidth;
        }
        else {
            SceneInteractive.main.DebugMsg("3:2");
            pop_XPOS = normal_XPOS - HalfWidth;
        }
    }

    public void ShowBag() {
        rectTransform.anchoredPosition = new Vector2(normal_XPOS,0);
        GetComponent<RectTransform>().DOAnchorPosX(pop_XPOS,1.0f);
    }

    public void HideBag() {
        rectTransform.anchoredPosition = new Vector2(pop_XPOS,0);
        GetComponent<RectTransform>().DOAnchorPosX(normal_XPOS,1.0f);
    }
}
