using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInteractive : MonoBehaviour {

	public static SceneInteractive main;

	public SceneStack sceneStack;
	public GameObject PrefabSelectedRect;
	[Space(10)]
	public TextOverlay objectTextOverlay;
	public ImageOverlay objectImageOverlay;
	public ScrollImageOverlay objectScrollImageOverlay;
	public GameObject UserBagPanelObject;
	public GameObject BagTitleObject;
	[Space(10)]
	public BagItem itemNowFocus;
	public GameObject rectNowSelected;

	private bool flagCondition;
	private AreaClass templateArea;

	public bool inIdle;

	[Space(10)]

	[SerializeField]
	AudioSource SNDGetItem;

	// Use this for initialization
	void Start () {
		main = this;
	}

	public void LogMessage(){
		Debug.Log ("Touch Area!");
	}

	public void GoToArea(AreaClass targetArea){
		AreaClass nowArea = sceneStack.GetLastAreaStack ();
		nowArea.gameObject.SetActive (false);
		targetArea.gameObject.SetActive (true);
		sceneStack.PushAreaStack (targetArea);
	}

	public void GoToAreaNoStack(AreaClass targetArea){
		AreaClass nowArea = sceneStack.GetLastAreaStack ();
		nowArea.gameObject.SetActive (false);
		targetArea.gameObject.SetActive (true);
		sceneStack.InsteadAreaStack(targetArea);
	}

	public void GoBackArea(){
		AreaClass nowArea = sceneStack.PullAreaStack ();
		if(nowArea == null)
			return;
		AreaClass previousArea = sceneStack.GetLastAreaStack ();
		nowArea.gameObject.SetActive (false);
		previousArea.gameObject.SetActive (true);
	}

	public void ShowTextOverlay(string src){
		objectTextOverlay.SetText (src);
		objectTextOverlay.gameObject.SetActive (true);
	}

	public void ShowImageOverlay(ImageOverlayClass src){
		objectImageOverlay.SetImage (src);
		objectImageOverlay.gameObject.SetActive (true);
	}

	public void ShowItemOverlay(BagItem src){
		GetItemOverlay.instance.SetItemInfo(src);
		GetItemOverlay.instance.gameObject.SetActive(true);
	}

	public void ShowScrollImageOverlay(ImageOverlayClass src){
		objectScrollImageOverlay.SetImage (src);
		objectScrollImageOverlay.gameObject.SetActive (true);
	}

	public void SwitchGameObjectActive(GameObject src){
		if (src.activeInHierarchy)
			src.SetActive (false);
		else
			src.SetActive (true);
	}

	public void FocusBagItem(BagItem src){
		// Select Rect Effect
		//if (rectNowSelected != null)
		//	Destroy (rectNowSelected);
		//rectNowSelected = Instantiate (PrefabSelectedRect, itemNowFocus.transform, false);


		/* Old Method Focus item
		ColorBlock _colors;

		if (itemNowFocus != null) {
			_colors = itemNowFocus.GetComponent<Button> ().colors;
			_colors.normalColor = new Color32 (128, 128, 128, 255);
			itemNowFocus.GetComponent<Button> ().colors = _colors;
		}

		_colors = itemNowFocus.GetComponent<Button> ().colors;
		_colors.normalColor = new Color32 (255, 255, 255, 255);
		itemNowFocus.GetComponent<Button> ().colors = _colors;
		*/

		itemNowFocus = src;
	}

	public void ShowPlayerBag(){
		UserBagPanelObject.GetComponent<PanelPlayerBag>().ShowBag();
	}

	public void HidePlayerBag(){
		UserBagPanelObject.GetComponent<PanelPlayerBag>().HideBag();
	}

	public void DestroyBagItem(BagItem src){
		if (rectNowSelected != null)
			Destroy (rectNowSelected);
		Destroy (src.gameObject);

		if (UserBagPanelObject.transform.childCount == 1) {
			BagTitleObject.GetComponent<BagTitle> ().CloseBag ();
			HidePlayerBag();
		}
	}

	public bool CheckBagHasItem(string itemName){
		bool flag = false;
		int count = 0;
		foreach (Transform child in UserBagPanelObject.transform) {
			BagItem item = child.GetComponent<BagItem> ();
			if (item != null) {
				if (item.bagName == itemName) {
					flag = true;
					count++;
				}
			}
		}

		return flag;
	}

	public void RemoveBagItem(string itemName){
		foreach (Transform child in UserBagPanelObject.transform) {
			BagItem item = child.GetComponent<BagItem> ();
			if (item != null) {
				if (item.bagName == itemName) {
					DestroyBagItem (item);
					return;
				}
			}
		}
	}

	public void DestroyNowFocusBagItem(){
		DestroyBagItem (itemNowFocus);
	}

	public void ItemUseTouchArea(BagItem item){
		flagCondition = false;
		if (item != null && itemNowFocus != null && item.bagName == itemNowFocus.bagName) {
			flagCondition = true;

			DestroyBagItem (itemNowFocus);
		}
	}

	public void ItemUseTouchAreaNoDestroy(BagItem item){
		flagCondition = false;
		if (item != null && itemNowFocus != null && item.bagName == itemNowFocus.bagName) {
			flagCondition = true;
		}
	}

	public void GoToAreaCondition(AreaClass targetArea){
		if (flagCondition)
			GoToArea (targetArea);
	}

	public void ShowImageOverlayCondition(ImageOverlayClass src){
		if (flagCondition)
			ShowImageOverlay (src);
	}

	public void GetItemNoSound(BagItem item){
		Instantiate (item, UserBagPanelObject.transform, false);
		BagTitleObject.GetComponent<BagTitle> ().OpenBag ();
		ShowPlayerBag();
	}

	public void GetItem(BagItem item){
		Instantiate (item, UserBagPanelObject.transform, false);
		BagTitleObject.GetComponent<BagTitle> ().OpenBag ();
		ShowPlayerBag();
		SNDGetItem.Play ();
	}

	public void SetInIdle(){
		StartCoroutine (IESetInIdle ());
	}

	IEnumerator IESetInIdle(){
		inIdle = true;
		yield return new WaitForSeconds (1.0f);
		inIdle = false;
	}


	public void SetTemplateAreaVariable(AreaClass area){
		templateArea = area;
	}
	public void ChangeGoToAreaTarget(Button targetGoto){
		targetGoto.onClick.RemoveAllListeners ();
		targetGoto.onClick.AddListener (() => GoToArea (templateArea));
	}
	public void RemoveButtonListener(Button button){
		button.onClick.RemoveAllListeners ();
	}


	// Flags

	public bool GetItemUseResult(){
		return flagCondition;
	}

	public void SetConditionFlagTo(bool src){
		flagCondition = src;
	}
		
}
