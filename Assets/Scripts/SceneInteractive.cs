using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using DG.Tweening;

public class SceneInteractive : MonoBehaviour {

	public static SceneInteractive main;

	public SceneStack sceneStack;
	public GameObject PrefabSelectedRect;
	public GameObject canvasScene;
	public VideoPlayer frontVideo;
	private CanvasScaler sceneScaler;
	private Vector2 defaultResolution;
	private Vector2 scaleTargetResolution;
	private Tween nowScalingTween;

	[Space(10)]
	public GameObject UserBagPanelObject;
	//public GameObject BagTitleObject;
	[Space(10)]
	public BagItem itemNowFocus;
	public GameObject rectNowSelected;

	private bool flagCondition;
	private AreaClass templateArea;

	public bool inIdle;

	[Space(10)]

	[SerializeField]
	AudioSource SNDGetItem = null;

	public float FadingSpeed;
	public float scaleSpeed;
	[Range(0,0.9f)]
	public float sceneScaleValue;

	public enum ZoomDirect {
		Go,
		Back
	}

	void Awake(){
		main = this;
	}

	// Use this for initialization
	void Start () {
		//Canvas Zoom
		sceneScaler = canvasScene.GetComponent<CanvasScaler>();
		defaultResolution = sceneScaler.referenceResolution;
		float valueTargetScaleX = defaultResolution.x;
		float valueTargetScaleY = (defaultResolution.y * (1 - sceneScaleValue));
		scaleTargetResolution = new Vector2(valueTargetScaleX,valueTargetScaleY);

	}

	public void LogMessage(){
		Debug.Log ("Touch Area!");
	}

	public void GoToAreaFade(AreaClass targetArea){
		AreaClass nowArea = sceneStack.GetLastAreaStack ();
		nowArea.gameObject.SetActive (false);
		targetArea.gameObject.SetActive (true);
		sceneStack.PushAreaStack (targetArea);

		targetArea.GetComponentInChildren<Image>().DOFade(0,2f).From();
	}

	///////////////////////////////////Go to Area Methods

	bool isAreaFading = false;
	public void GoToArea(AreaClass targetArea){
		if(isAreaFading == true)
			return;

		isAreaFading = true;
		StartCoroutine(FadeToAction(FadingSpeed,ZoomDirect.Go,delegate {
			AreaClass nowArea = sceneStack.GetLastAreaStack ();
			nowArea.gameObject.SetActive (false);
			targetArea.gameObject.SetActive (true);
			sceneStack.PushAreaStack (targetArea);
			isAreaFading = false;
		}));
	}
	public void GoToAreaNoStack(AreaClass targetArea){
		if(isAreaFading == true)
			return;

		isAreaFading = true;
		StartCoroutine(FadeToAction(FadingSpeed,ZoomDirect.Go,delegate {
			AreaClass nowArea = sceneStack.GetLastAreaStack ();
			nowArea.gameObject.SetActive (false);
			targetArea.gameObject.SetActive (true);
			sceneStack.InsteadAreaStack(targetArea);
			isAreaFading = false;
		}));
	}
	public void GoBackArea(){
		if(isAreaFading == true)
			return;

		AreaClass nowArea = sceneStack.PullAreaStack ();
		//Prevent no Area to Back bug
		if(nowArea == null)
			return;

		isAreaFading = true;
		StartCoroutine(FadeToAction(FadingSpeed,ZoomDirect.Back,delegate {
			AreaClass previousArea = sceneStack.GetLastAreaStack ();
			nowArea.gameObject.SetActive (false);
			previousArea.gameObject.SetActive (true);
			isAreaFading = false;
		}));
	}
	IEnumerator FadeToAction(float duration,ZoomDirect dir, System.Action runAction){
		BlackMask.MaskShow(duration);
		yield return new WaitForSeconds(duration);
		runAction();
		ZoomCanvas(dir);
		BlackMask.MaskHide(duration);
	}

	void ZoomCanvas(ZoomDirect dir){
		
		switch(dir){
			case ZoomDirect.Go :
			if(nowScalingTween != null)
				nowScalingTween.Kill();
			sceneScaler.referenceResolution = defaultResolution;
			nowScalingTween = DOTween.To(()=> sceneScaler.referenceResolution, x => sceneScaler.referenceResolution = x, scaleTargetResolution, scaleSpeed);
			
			break;

			case ZoomDirect.Back :
			if(nowScalingTween != null)
				nowScalingTween.Kill();
			sceneScaler.referenceResolution = scaleTargetResolution;
			nowScalingTween = DOTween.To(()=> sceneScaler.referenceResolution, x => sceneScaler.referenceResolution = x, defaultResolution, scaleSpeed);
			break;

			default:
			break;
		}
	}



	//////////////////////////////////

	public void ShowTextOverlay(string src){
		TextOverlay.main.SetText (src);
		TextOverlay.main.gameObject.SetActive (true);
	}

	public void ShowImageOverlay(ImageOverlayClass src){
		ImageOverlay.main.SetImage (src);
		ImageOverlay.main.gameObject.SetActive (true);
	}

	public void ShowItemOverlay(BagItem src){
		GetItemOverlay.main.SetItemInfo(src);
		GetItemOverlay.main.gameObject.SetActive(true);
	}

	public void ShowScrollImageOverlay(ImageOverlayClass src){
		ScrollImageOverlay.main.SetImage (src);
		ScrollImageOverlay.main.gameObject.SetActive (true);
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
			//BagTitleObject.GetComponent<BagTitle> ().CloseBag ();
			HidePlayerBag();
		}
	}

	public int GetBagItemNumber(){
		int count = 0;
		foreach (Transform child in UserBagPanelObject.transform) {
			BagItem item = child.GetComponent<BagItem> ();
			if (item != null) {
				count++;
			}
		}
		return count;
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
		if(GetBagItemNumber() == 0)
			ShowPlayerBag();
		Instantiate (item, UserBagPanelObject.transform, false);
		//BagTitleObject.GetComponent<BagTitle> ().OpenBag ();
		
	}

	public void GetItem(BagItem item){
		if(GetBagItemNumber() == 0)
			ShowPlayerBag();
		Instantiate (item, UserBagPanelObject.transform, false);
		//BagTitleObject.GetComponent<BagTitle> ().OpenBag ();
		
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


	public void VideoPause(){
		if(frontVideo != null)
			frontVideo.Pause();
	}

	public void VideoResume(){
		if(frontVideo != null)
			frontVideo.Play();
	}

	public void DebugMsg(string msg){
		if(OverlayDebug.main != null)
			OverlayDebug.main.QueueString(msg);
	}


	// Flags

	public bool GetItemUseResult(){
		return flagCondition;
	}

	public void SetConditionFlagTo(bool src){
		flagCondition = src;
	}
		
}
