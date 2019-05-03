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

    public bool ResizeTexture;

    public float ResizeRate;

    void Awake(){
        targetImage = GetComponent<RawImage>();
        sourceVideo = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetImage.color = new Color(1,1,1,0);

        int rW = Mathf.FloorToInt(targetImage.rectTransform.rect.width);
        int rH = Mathf.FloorToInt(targetImage.rectTransform.rect.height);

        if(ResizeTexture){
            rW = Mathf.FloorToInt(rW * ResizeRate);
            rH = Mathf.FloorToInt(rH * ResizeRate);
        }

        renderTexture = sourceVideo.targetTexture;
        if (renderTexture==null){
            renderTexture = new RenderTexture(rW, rH, 0);
            sourceVideo.targetTexture = renderTexture;
            //StartCoroutine(SettingTexture());
        }
        targetImage.texture = renderTexture;

        StartCoroutine(SetupRawImageWhenPlay());

        if(StayFirstFrame){
            sourceVideo.Play();
            StartCoroutine(FramePauseInFirstFrame());
        }
    }

    IEnumerator SetupRawImageWhenPlay(){
        yield return new WaitUntil(()=>{return sourceVideo.isPlaying;});
        yield return null;          //When start Playing , First Frame is always Black in Android , so need to skip
        targetImage.enabled = true;
        targetImage.color = new Color(1,1,1,1);
        SceneInteractive.main.DebugMsg(gameObject.name + " set finished in " + Time.time);
    }

    IEnumerator FramePauseInFirstFrame(){
        yield return new WaitUntil(()=>{return sourceVideo.isPlaying;});
        yield return null;          //When start Playing , First Frame is always Black in Android , so need to skip
        targetImage.enabled = true;
        targetImage.color = new Color(1,1,1,1);
        sourceVideo.Pause();
    }

    IEnumerator SettingTexture(){
        yield return new WaitUntil(()=>{return sourceVideo.texture != null;});
        targetImage.texture = sourceVideo.texture;
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
