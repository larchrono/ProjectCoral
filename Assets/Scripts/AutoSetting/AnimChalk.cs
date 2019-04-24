using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimChalk : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.DOFillAmount(1,1);
    }
}
