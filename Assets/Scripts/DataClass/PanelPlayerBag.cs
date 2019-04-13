using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelPlayerBag : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowBag() {
        rectTransform.anchoredPosition = new Vector2(220,0);
        GetComponent<RectTransform>().DOAnchorPosX(0,1.0f);
    }

    public void HideBag() {
        rectTransform.anchoredPosition = new Vector2(0,0);
        GetComponent<RectTransform>().DOAnchorPosX(220,1.0f);
    }
}
