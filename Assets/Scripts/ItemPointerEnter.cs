using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPointerEnter : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
	public Text _label;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("Name: " + eventData.pointerCurrentRaycast.gameObject.name);
		//Debug.Log("Tag: " + eventData.pointerCurrentRaycast.gameObject.tag);
		//Debug.Log("GameObject: " + eventData.pointerCurrentRaycast.gameObject);

		//_label.text = eventData.pointerCurrentRaycast.gameObject.name;
	}

	public void OnPointerExit(PointerEventData eventData) {
		
		//Debug.Log("Name: " + eventData.pointerCurrentRaycast.gameObject.name);
		//Debug.Log("Tag: " + eventData.pointerCurrentRaycast.gameObject.tag);
		//Debug.Log("GameObject: " + eventData.pointerCurrentRaycast.gameObject);
		_label.text = "m:" + Random.Range (0, 100);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log ("DWW");
	}

	public void OnPointerUp (PointerEventData eventData){
		Debug.Log ("UPP");
		_label.text = "m:" + Random.Range (0, 100);
	}
}
