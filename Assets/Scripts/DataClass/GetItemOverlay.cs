using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemOverlay : MonoBehaviour {

	public static GetItemOverlay main;
	public event EventHandler closeOverlay;

	public Button Button_Background;
	public float TimeItemStay;

    [SerializeField]
    Text TextItemName = null;

    [SerializeField]
    Image ImageItemIcon = null;

	void Awake() {
		main = this;
		gameObject.SetActive(false);
	}

	public void OnCloseOverlay(){
		if(closeOverlay != null){
			closeOverlay(this, new EventArgs());
		}
	}

	public void SetItemInfo(BagItem src){
        TextItemName.text = src.bagName;
        ImageItemIcon.sprite = src.GetComponent<Image>().sprite;
		gameObject.SetActive(true);
		Button_Background.interactable = false;
		StartCoroutine(ResumeInteractive());
	}

	IEnumerator ResumeInteractive(){
		yield return new WaitForSeconds(TimeItemStay);
		Button_Background.interactable = true;
	}
}
