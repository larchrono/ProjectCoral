using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOverlay : MonoBehaviour {

	public Text textShow;

	public void SetText(string src){
		textShow.text = src;
	}
}
