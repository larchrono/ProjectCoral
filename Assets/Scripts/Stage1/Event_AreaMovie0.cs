using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Event_AreaMovie0 : MonoBehaviour
{

    [SerializeField]
    AreaClass nextArea;

    [SerializeField]
    VideoPlayer introVideo;

    // Start is called before the first frame update
    void Start()
    {
        introVideo.loopPointReached += IntroEndReached;
    }

    void IntroEndReached(VideoPlayer vp)
    {
        SceneInteractive.main.GoToAreaNoStack(nextArea);
    }
}
