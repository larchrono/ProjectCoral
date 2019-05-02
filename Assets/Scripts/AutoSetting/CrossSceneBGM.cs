using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrossSceneBGM : MonoBehaviour
{
    public static CrossSceneBGM instance;

	public float BGMVolume;
	public float BGMMovieVolume;
	public float FadeInSecond;

	public AudioClip MainBGM;
	public AudioClip MovieBGM;

	const float RapidSwitchTime = 1f;

	AudioSource _bgm;

    void Awake(){
        if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
    }

	void Start(){
		_bgm = GetComponent<AudioSource>();
		_bgm.volume = 0;
		_bgm.DOFade(BGMVolume,FadeInSecond).SetEase(Ease.Linear);
	}

	public void PauseBGM(){
		_bgm.DOFade(0,RapidSwitchTime).SetEase(Ease.Linear);
	}

	public void ResumeBGM(){
		_bgm.DOFade(BGMVolume,RapidSwitchTime).SetEase(Ease.Linear);
	}

	public void FadeToMovieBGM(){
		Sequence seq = DOTween.Sequence()
		.Append(_bgm.DOFade(0,RapidSwitchTime).SetEase(Ease.Linear))
		.AppendCallback(()=>{
			_bgm.Stop();
			_bgm.clip = MovieBGM;
			_bgm.Play();
		})
		.Append(_bgm.DOFade(BGMMovieVolume,RapidSwitchTime).SetEase(Ease.Linear));
	}

	public void FadeToMainBGM(){
		Sequence seq = DOTween.Sequence()
		.Append(_bgm.DOFade(0,RapidSwitchTime).SetEase(Ease.Linear))
		.AppendCallback(()=>{
			_bgm.Stop();
			_bgm.clip = MainBGM;
			_bgm.Play();
		})
		.Append(_bgm.DOFade(BGMVolume,RapidSwitchTime).SetEase(Ease.Linear));
	}
}