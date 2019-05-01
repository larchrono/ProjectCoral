using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeFactorBGM : MonoBehaviour
{
    float originVolume;
    AudioSource _bgm;

    bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        _bgm = GetComponent<AudioSource>();
        originVolume = _bgm.volume;
    }

    public void TriggerAudioLoud(){
        isFading = true;
        Sequence seq = DOTween.Sequence()
        .Append(_bgm.DOFade(1,0.5f))
        .AppendInterval(3.0f)
        .Append(_bgm.DOFade(originVolume,0.5f))
        .OnComplete(()=>{
            isFading = false;
        });
    }
}
