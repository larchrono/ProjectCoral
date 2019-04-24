using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class Event_AreaMovie1 : MonoBehaviour
{

    [SerializeField]
    VideoPlayer BackgroundVideo = null;

    [SerializeField]
    VideoPlayer introMovie = null;

    void Start(){
        SceneInteractive.main.frontVideo = BackgroundVideo;
    }

    public void AddOverlayCloseAction(){
        GetItemOverlay.main.closeOverlay += OnCloseOverlay;
    }

    public void OnCloseOverlay(object sender, EventArgs e){
        StartCoroutine(FadeToNext());
        GetItemOverlay.main.closeOverlay -= OnCloseOverlay;
    }

    IEnumerator FadeToNext(){
        SceneInteractive.main.HidePlayerBag();

        yield return new WaitForSeconds(2f);

        BackgroundVideo.Pause();
        RawImage _rawImage = BackgroundVideo.GetComponent<RawImage>();
        _rawImage.DOFade(0,1.5f);

        introMovie.gameObject.SetActive(true);
        introMovie.loopPointReached += EndIntroMovie;

        SceneInteractive.main.frontVideo = introMovie;

        //RawImage _rawImage = introMovie.GetComponent<RawImage>();
        //_rawImage.DOFade(1,1.5f);
    }

    void EndIntroMovie(VideoPlayer vp){
        //sceneInteractive.GoToAreaNoStack(nextArea);
        IndexMenu.main.LoadScene("Scene1");
    }
}
