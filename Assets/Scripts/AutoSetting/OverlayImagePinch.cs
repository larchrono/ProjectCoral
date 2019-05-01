using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayImagePinch : MonoBehaviour
{
    private bool IsZooming = false;
    private float DoubleTouchCurrDis;
    private float DoubleTouchLastDis;
    private Vector3 originSize;
    private RectTransform rectTransform;
    private ScrollRect scrollRect;

    public float ScaleRate;
    public Vector3 maxScaleSize;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scrollRect = GetComponentInParent<ScrollRect>();
        originSize = rectTransform.localScale;
    }

    void Update()
    {
        if ((Input.touchCount > 1) && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            DoubleTouchCurrDis = Vector2.Distance(touch1.position, touch2.position);

            if (!IsZooming)
            {
                DoubleTouchLastDis = DoubleTouchCurrDis;
                IsZooming = true;
                scrollRect.enabled = false;
            }

            float distance = DoubleTouchCurrDis - DoubleTouchLastDis;
            float targetValue = (distance >= 0) ? 1 : -1;

            Vector3 targetSize = rectTransform.localScale * (1 + ScaleRate * targetValue);

            if (targetSize.magnitude >= originSize.magnitude && targetSize.magnitude <= maxScaleSize.magnitude)
            {
                rectTransform.localScale = targetSize;
            }

            //使用相對資訊來縮放
            DoubleTouchLastDis = DoubleTouchCurrDis;
        }
        if (Input.touchCount < 2)
        {
            IsZooming = false;
            scrollRect.enabled = true;
        }
    }

    public void ResetSize()
    {
        rectTransform.localScale = Vector3.one;
    }

    public void SetScaleRate(float src)
    {
        ScaleRate = src;
    }
}
