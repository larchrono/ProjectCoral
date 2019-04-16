using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollImageOverlay : MonoBehaviour
{
	public static ScrollImageOverlay main;

	public ImageOverlayClass imageShow;

	[SerializeField]
	private ScrollRect Content = null;

	//Need set Active in Scene or Set it into Init scripts
	void Awake() {
		main = this;
		gameObject.SetActive(false);
	}

	public void SetImage(ImageOverlayClass src){
		if (imageShow != null)
			Destroy (imageShow.gameObject);
		imageShow = Instantiate (src.gameObject, Content.transform, false).GetComponent<ImageOverlayClass>();
		Content.content = imageShow.GetComponent<RectTransform> ();
	}
}
