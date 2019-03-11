using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour , IDragHandler, IBeginDragHandler , IEndDragHandler
{
	RectTransform _canvas;

	void Awake(){
		_canvas = FindInParents<Canvas>(gameObject).GetComponent<RectTransform>();
	}

	public void OnDrag(PointerEventData eventData){
		//Debug.Log (eventData.position);
		//RectTransformUtility.ScreenPointToLocalPointInRectangle ();

		SetDraggedPosition(eventData);
	}

	public void OnBeginDrag (PointerEventData eventData){
		//gameObject.transform.SetAsLastSibling ();

		//Debug.Log ("Drag Start");
		SetDraggedPosition(eventData);
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (_canvas == null)
			return;

		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, data.position, data.pressEventCamera, out globalMousePos))
		{
			//it can be accurately set anchoredPosition
			gameObject.transform.position = globalMousePos;

			//Debug.Log (globalMousePos);
		}
	}

	public void OnEndDrag (PointerEventData eventData){

	}

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;

		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
}
