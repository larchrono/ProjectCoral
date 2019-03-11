using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIrregular : MonoBehaviour
{
	float threshold = 0.1f;

    // Start is called before the first frame update
    void Start() {
		GetComponent<Image> ().alphaHitTestMinimumThreshold = threshold;

		// it can set image to invisible effectly
		//GetComponent<CanvasRenderer> ().cull = true;
    }
}
