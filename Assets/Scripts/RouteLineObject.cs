using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteLineObject : MonoBehaviour {

	public Sprite NormalLine;
	public Sprite ErrorLine;

	public void ConvertToError(){
		Image img = GetComponent<Image> ();
		if (img != null)
			img.sprite = ErrorLine;
	}
}
