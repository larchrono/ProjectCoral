using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoEndCallback : MonoBehaviour
{
    VideoPlayer _vPlayer;

    public QueueAction WhenPlayEnd;

    // Start is called before the first frame update
    void Start()
    {
        _vPlayer = GetComponent<VideoPlayer>();
        _vPlayer.loopPointReached += DoWhenPlayEnd;
    }

    void DoWhenPlayEnd(VideoPlayer vp){
        WhenPlayEnd.Invoke();
    }
}
