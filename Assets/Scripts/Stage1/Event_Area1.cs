using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class Event_Area1 : MonoBehaviour
{
    [SerializeField]
    AudioSource _bgm = null;
    [SerializeField]
    Image PreBlackView = null;
    [SerializeField]
    VideoPlayer PreMovie = null;

    public QueueAction EyeOpenFinished;

    bool hasRunIntrol;

    // Start is called before the first frame update
    void Start()
    {
        if(!hasRunIntrol){
            DOTween.To(()=> _bgm.volume, x => _bgm.volume = x , 0.7f , 5);
            PreMovie.started += RemovePreBlackView;
            PreMovie.loopPointReached += PreMovieEnd;
            hasRunIntrol = true;
            SceneInteractive.main.DebugMsg("Area1 _ Start");
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
        EyeOpenFinished.Invoke();
    }

}
