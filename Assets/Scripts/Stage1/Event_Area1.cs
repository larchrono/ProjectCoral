using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class Event_Area1 : MonoBehaviour
{
    [SerializeField]
    AudioSource _bgm;
    [SerializeField]
    Image PreBlackView;
    [SerializeField]
    VideoPlayer PreMovie;

    [SerializeField]
    GameObject ButtonBackArea;

    bool hasRunIntrol;

    // Start is called before the first frame update
    void Start()
    {
        if(!hasRunIntrol){
            DOTween.To(()=> _bgm.volume, x => _bgm.volume = x , 0.7f , 5);
            PreMovie.started += RemovePreBlackView;
            PreMovie.loopPointReached += PreMovieEnd;
            hasRunIntrol = true;
        }
    }

    IEnumerator RemovePreBlackView(){
        while(PreMovie.time < 0.1f){
            yield return null;
        }
        PreBlackView.gameObject.SetActive(false);
    }

    void RemovePreBlackView(VideoPlayer vp){
        StartCoroutine(RemovePreBlackView());
    }

    void PreMovieEnd(VideoPlayer vp){
        SceneInteractive.main.ShowPlayerBag();
        ButtonBackArea.SetActive(true);
        PreMovie.gameObject.SetActive(false);
    }

}
