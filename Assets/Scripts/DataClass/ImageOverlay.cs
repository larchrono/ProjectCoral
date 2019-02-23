using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOverlay : MonoBehaviour {

	public ImageOverlayClass imageShow;

	public void SetImage(ImageOverlayClass src){
		if (imageShow != null)
			Destroy (imageShow.gameObject);
		imageShow = Instantiate (src.gameObject, transform, false).GetComponent<ImageOverlayClass>();
	}
}
