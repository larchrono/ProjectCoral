using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsedSpritePool : MonoBehaviour
{
    public Sprite [] UsedSprite;

    public void SetSpriteToPoolID(int id){
        Image _img = GetComponent<Image>();

        if(_img == null)
            return;
        
        if(id >= UsedSprite.Length)
            return;

        _img.overrideSprite = UsedSprite[id];
        //_img.sprite = UsedSprite[id];
    }
}
