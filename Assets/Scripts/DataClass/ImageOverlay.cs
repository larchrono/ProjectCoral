
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOverlay : MonoBehaviour {

	public static ImageOverlay instance;

	public event EventHandler closeOverlay;

	public ImageOverlayClass imageShow;

	void Awake() {
		instance = this;
	}

	public void OnCloseOverlay(){
		if(closeOverlay != null){
			closeOverlay(this, new EventArgs());
		}
	}

	public void SetImage(ImageOverlayClass src){
		if (imageShow != null)
			Destroy (imageShow.gameObject);
		imageShow = Instantiate (src.gameObject, transform, false).GetComponent<ImageOverlayClass>();
	}
}
