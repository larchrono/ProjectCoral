using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemDragUse : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler , IPointerDownHandler , IPointerUpHandler
{
    const int DraggingItemWidth = 200;
    const int DraggingItemHeight = 200;
    const int DraggingItemSlideUp = 50;

    const float RateTouchScale = 1.3f;
    const float RateMoveScale = 1.5f;

    private RectTransform _canvas;
    private Image _image;
    private Vector2 originSize;
    private GameObject m_DraggingIcon;

    private bool _beginDrag;

    private bool effectiveUse = false;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = FindInParents<Canvas>(gameObject).GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        Invoke("LateStart",0.1f);
    }

    void LateStart(){
        originSize = GetComponent<RectTransform>().sizeDelta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canvas == null)
            return;

        m_DraggingIcon = new GameObject("tempIcon");

        m_DraggingIcon.transform.SetParent(_canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();
        m_DraggingIcon.transform.position = transform.position;

        var image = m_DraggingIcon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.rectTransform.sizeDelta = originSize * RateTouchScale;
        image.raycastTarget = false;
        image.color = new Color(1, 1, 1, 0.7f);

        _image.enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData){
        if(_beginDrag == true)
            return;

        _image.enabled = true;
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_canvas == null)
            return;
        _beginDrag = true;

        var image = m_DraggingIcon.GetComponent<Image>();
        image.rectTransform.sizeDelta = originSize * RateMoveScale;

        SetDraggedPosition(eventData);

        //It will Focus Bag Item, important !
        Button thisButton = GetComponent<Button>();
        if (thisButton != null)
        {
            thisButton.onClick.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canvas == null)
            return;
        if (m_DraggingIcon == null)
            return;

        SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(effectiveUse){
            RemoveTempIcon();
        }
        else {
            m_DraggingIcon.transform.DOMove(transform.position,0.5f).OnComplete(RemoveTempIcon);
            m_DraggingIcon.GetComponent<RectTransform>().DOSizeDelta(originSize,0.5f);
        }
        effectiveUse = false;
    }

    void RemoveTempIcon(){
        _beginDrag = false;

        if(_image != null)
            _image.enabled = true;
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);
    }

    public void EffectiveUse(){
        Debug.Log("Effective Use");
        effectiveUse = true;
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;

            //Up slide to let user see the item on his finger
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + DraggingItemSlideUp);
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
