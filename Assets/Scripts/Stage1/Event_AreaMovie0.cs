using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Event_AreaMovie0 : MonoBehaviour
{
    [Header("Setting Video callback to NextArea")]

    [SerializeField]
    AreaClass nextArea = null;

    [SerializeField]
    VideoPlayer introVideo = null;

    // Start is called before the first frame update
    void Start()
    {
        introVideo.loopPointReached += IntroEndReached;
        SceneInteractive.main.frontVideo = introVideo;
    }

    void IntroEndReached(VideoPlayer vp)
    {
        //SceneInteractive.main.GoToAreaNoStack(nextArea);
        IndexMenu.main.LoadSceneWithFade("Scene1");
    }
}
