using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundSetting : MonoBehaviour
{
    UsedSpritePool _pool;
    Image _icon;

    Sprite _openSound;
    Sprite _closeSound;

    Animator _anim;

    void Start() {

        _pool = GetComponent<UsedSpritePool>();
        _icon = GetComponent<Image>();
        _anim = GetComponent<Animator>();

        _openSound = _pool.UsedSprite[0];
        _closeSound = _pool.UsedSprite[1];

        bool soundSetting = GlobalVariables.instance.isUseSound;

        if(soundSetting)
            _icon.sprite = _openSound;
        else 
            _icon.sprite = _closeSound;
        
        if(_anim != null)
            _anim.enabled = true;
    }

    public void SoundSwitch(){

        bool soundSetting = GlobalVariables.instance.isUseSound;

        if(soundSetting){
            _icon.sprite = _closeSound;
            GlobalVariables.instance.SetUseSound(!soundSetting);
            AudioListener.volume = 0;
        } else {
            _icon.sprite = _openSound;
            GlobalVariables.instance.SetUseSound(!soundSetting);
            AudioListener.volume = 1;
        }
	}
}
