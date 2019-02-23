using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchAreaSetting : MonoBehaviour {

	Image Image_AreaRect;

	// Use this for initialization
	void Start () {
		Image_AreaRect = GetComponent<Image> ();
		if (Image_AreaRect == null)
			return;

		Color32 sourceColor = Image_AreaRect.color;
		if (GlobalVariables.instance.isShowTouchAreaColor) {
			Image_AreaRect.color = new Color32 (sourceColor.r, sourceColor.g, sourceColor.b, 50);
		} else {
			Image_AreaRect.color = new Color32 (sourceColor.r, sourceColor.g, sourceColor.b, 0);
		}

	}

}
