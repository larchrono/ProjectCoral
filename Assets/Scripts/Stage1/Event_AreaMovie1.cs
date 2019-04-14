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
    VideoPlayer BackgroundVideo;

    [SerializeField]
    VideoPlayer introMovie;

    public void AddOverlayCloseAction(){
        GetItemOverlay.instance.closeOverlay += OnCloseOverlay;
    }

    public void OnCloseOverlay(object sender, EventArgs e){
        StartCoroutine(FadeToNext());
        GetItemOverlay.instance.closeOverlay -= OnCloseOverlay;
    }

    IEnumerator FadeToNext(){
        SceneInteractive.main.HidePlayerBag();

        yield return new WaitForSeconds(2f);

        BackgroundVideo.Pause();
        RawImage _rawImage = BackgroundVideo.GetComponent<RawImage>();
        _rawImage.DOFade(0,1.5f);

        introMovie.gameObject.SetActive(true);
        introMovie.loopPointReached += EndIntroMovie;

        //RawImage _rawImage = introMovie.GetComponent<RawImage>();
        //_rawImage.DOFade(1,1.5f);
    }

    void EndIntroMovie(VideoPlayer vp){
        //sceneInteractive.GoToAreaNoStack(nextArea);
        IndexMenu.main.LoadScene("Scene1");
    }
}
