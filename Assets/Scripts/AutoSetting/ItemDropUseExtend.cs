using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDropUseExtend : MonoBehaviour, IDropHandler
{
    public BagItem TargetItem;
    public bool ShouldRemoveItem;
    public QueueAction UseItemSuccessAction;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        BagItem itemNowFocus = SceneInteractive.main.itemNowFocus;
		if (TargetItem != null && itemNowFocus != null && TargetItem.bagName == itemNowFocus.bagName) {
            if(ShouldRemoveItem){
                StartCoroutine(RunRemoveItem());
                //Debug.Log("Remove Start");
            }
            StartCoroutine(DropDelay());
		}

        var _item = eventData.pointerDrag.GetComponent<ItemDragUse>();
        if(_item != null){
            _item.EffectiveUse();
        }

        //Debug.Log (eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition.y);
    }

    IEnumerator RunRemoveItem(){
        yield return null;
        //Debug.Log("Remove Start 2");
        SceneInteractive.main.RemoveBagItem(TargetItem.bagName);
    }

    IEnumerator DropDelay()
    {
        yield return null;
        UseItemSuccessAction.Invoke();
    }
}
