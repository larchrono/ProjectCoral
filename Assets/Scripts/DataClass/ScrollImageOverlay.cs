using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollImageOverlay : MonoBehaviour
{
	public ImageOverlayClass imageShow;

	[SerializeField]
	private ScrollRect Content = null;

	public void SetImage(ImageOverlayClass src){
		if (imageShow != null)
			Destroy (imageShow.gameObject);
		imageShow = Instantiate (src.gameObject, Content.transform, false).GetComponent<ImageOverlayClass>();
		Content.content = imageShow.GetComponent<RectTransform> ();
	}
}
