using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class Event_Stage2_Area0 : MonoBehaviour
{

    [SerializeField]
    Image PreBlackView;
    [SerializeField]
    VideoPlayer PreMovie;

    bool hasRunIntrol;

    // Start is called before the first frame update
    void Start()
    {
        if(!hasRunIntrol){
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
        PreMovie.gameObject.SetActive(false);
    }
}
