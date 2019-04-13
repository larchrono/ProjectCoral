using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragUse : MonoBehaviour , IDragHandler, IBeginDragHandler , IEndDragHandler
{
	const int DraggingItemWidth = 175;
	const int DraggingItemHeight = 175;
	const int DraggingItemSlideUp = 50;

	private RectTransform _canvas;
	private GameObject m_DraggingIcon;

    // Start is called before the first frame update
    void Start() {
		_canvas = FindInParents<Canvas>(gameObject).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnBeginDrag (PointerEventData eventData){
		if (_canvas == null)
			return;

		m_DraggingIcon = new GameObject ("tempIcon");

		m_DraggingIcon.transform.SetParent (_canvas.transform, false);
		m_DraggingIcon.transform.SetAsLastSibling ();

		var image = m_DraggingIcon.AddComponent<Image> ();
		image.sprite = GetComponent<Image> ().sprite;
		image.rectTransform.sizeDelta = new Vector2 (DraggingItemWidth, DraggingItemHeight);
		image.raycastTarget = false;
		image.color = new Color (1, 1, 1, 0.7f);

		SetDraggedPosition(eventData);

		Button thisButton = GetComponent<Button> ();
		if (thisButton != null) {
			thisButton.onClick.Invoke ();
		}
	}

	public void OnDrag(PointerEventData eventData){
		if (_canvas == null)
			return;
		if (m_DraggingIcon == null)
			return;
		
		SetDraggedPosition(eventData);
	}

	public void OnEndDrag (PointerEventData eventData){
		if (m_DraggingIcon != null)
			Destroy (m_DraggingIcon);
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		var rt = m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, data.position, data.pressEventCamera, out globalMousePos))
		{
			rt.position = globalMousePos;

			//Up slide to let user see the item on his finger
			rt.anchoredPosition = new Vector2 (rt.anchoredPosition.x, rt.anchoredPosition.y + DraggingItemSlideUp);
		}
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
