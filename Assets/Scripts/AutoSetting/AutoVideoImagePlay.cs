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

    // Start is called before the first frame update
    void Start()
    {
        targetImage = GetComponent<RawImage>();
        sourceVideo = GetComponent<VideoPlayer>();

        targetImage.enabled = true;

        int rW = Mathf.FloorToInt(targetImage.rectTransform.rect.width);
        int rH = Mathf.FloorToInt(targetImage.rectTransform.rect.height);

        renderTexture = sourceVideo.targetTexture;

        if (renderTexture==null){
            renderTexture = new RenderTexture(rW, rH, 24);
            sourceVideo.targetTexture = renderTexture;
        }
            
        targetImage.texture = renderTexture;
        
    }

    void OnDisable() {
        renderTexture.Release();
    }
}
