using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AutoVideoImagePlay : MonoBehaviour
{

    RawImage targetImage;
    VideoPlayer sourceVideo;
    RenderTexture renderTexture;

    public bool dontAutoRelease;

    public bool StayFirstFrame;

    void Awake(){
        targetImage = GetComponent<RawImage>();
        sourceVideo = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetImage.enabled = true;

        int rW = Mathf.FloorToInt(targetImage.rectTransform.rect.width);
        int rH = Mathf.FloorToInt(targetImage.rectTransform.rect.height);

        renderTexture = sourceVideo.targetTexture;

        if (renderTexture==null){
            renderTexture = new RenderTexture(rW, rH, 24);
            sourceVideo.targetTexture = renderTexture;
        }
            
        targetImage.texture = renderTexture;

        if(StayFirstFrame){
            sourceVideo.Play();
            StartCoroutine(FramePause());
        }
    }

    IEnumerator FramePause(){
        yield return null;
        sourceVideo.Pause();
    }

    void OnDisable() {
        if(dontAutoRelease)
            return;
        renderTexture.Release();
    }

    public void ResetVideoAndPlay(){
        sourceVideo.Stop();
        //sourceVideo.time = 0;
        sourceVideo.Play();
    }
}
