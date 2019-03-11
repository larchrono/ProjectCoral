﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDropUse : MonoBehaviour, IDropHandler
{
	const float _delayPara = 0.1f;
	Button _button;
    // Start is called before the first frame update
    void Start() {
		_button = GetComponent<Button> ();
    }

	public void OnDrop (PointerEventData eventData){
		if (_button == null)
			return;

		StartCoroutine (DropDelay());

		//Debug.Log (eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition.y);
	}

	IEnumerator DropDelay(){
		yield return new WaitForSeconds (_delayPara);
		_button.onClick.Invoke ();
	}
}