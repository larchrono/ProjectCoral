using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageClass : MonoBehaviour
{
    public Sprite SpriteDefault;
    public Sprite SpriteFinished;

    public int index;

    Image _image;

    void Awake() {
        _image = GetComponent<Image>();
    }

    void Start(){

    }

    public void SetPageAttribute(bool isGet){
        if(_image == null)
            _image = GetComponent<Image>();
        if(isGet)
            _image.overrideSprite = SpriteFinished;
        else
            _image.overrideSprite = SpriteDefault;
    }
}
