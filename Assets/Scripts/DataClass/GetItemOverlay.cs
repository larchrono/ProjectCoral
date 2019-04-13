using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemOverlay : MonoBehaviour {

	public static GetItemOverlay instance;
	public event EventHandler closeOverlay;

    [SerializeField]
    Text TextItemName;

    [SerializeField]
    Image ImageItemIcon;

	void Awake() {
		instance = this;
	}

	public void OnCloseOverlay(){
		if(closeOverlay != null){
			closeOverlay(this, new EventArgs());
		}
	}

	public void SetItemInfo(BagItem src){
        TextItemName.text = src.bagName;
        ImageItemIcon.sprite = src.GetComponent<Image>().sprite;
	}
}
