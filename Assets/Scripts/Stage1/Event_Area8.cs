using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Event_Area8 : MonoBehaviour
{
    [Header("Release Texture and Add End movie callback")]
    public VideoPlayer endMoive;
    public RenderTexture targetTexture;

    // Start is called before the first frame update
    void Start()
    {   
        targetTexture.Release();
        endMoive.loopPointReached += EndMoviePlay;
        SceneInteractive.main.frontVideo = endMoive;
    }

    void EndMoviePlay(VideoPlayer vp){
        IndexMenu.main.LoadSceneDelay("Scene2",2);
    }
}
