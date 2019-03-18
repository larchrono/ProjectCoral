using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestCatch : MonoBehaviour
{
	DOTweenAnimation anim;

    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<DOTweenAnimation> ();

		anim.DOPlayForward ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
