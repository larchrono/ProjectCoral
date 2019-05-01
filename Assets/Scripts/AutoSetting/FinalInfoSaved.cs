using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalInfoSaved : MonoBehaviour
{
    public Vector2 finalPosition;
    Vector2 originPosition;

    public Color32 finalColor;
    Color32 originColor;

    Image _image;
    RawImage _rawImage;

    void Awake() {
        _image = GetComponent<Image>();
        _rawImage = GetComponent<RawImage>();

        originPosition = GetComponent<RectTransform>().anchoredPosition;
        if(_image != null)
            originColor = _image.color;
        if(_rawImage != null)
            originColor = _rawImage.color;
    }

    public void SetPosToFinalPosition(){
        GetComponent<RectTransform>().anchoredPosition = finalPosition;
    }

    public void SetPosToOriginPosition(){
        GetComponent<RectTransform>().anchoredPosition = originPosition;
    }

    public void SetColorToFinalColor(){
        if(_image != null)
            _image.color = finalColor;

        if(_rawImage != null)
            _rawImage.color = finalColor;
    }

    public void SetColorToOriginColor(){
        if(_image != null)
            _image.color = originColor;

        if(_rawImage != null)
            _rawImage.color = originColor;
    }
}
