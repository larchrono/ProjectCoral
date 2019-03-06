using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Area4 : MonoBehaviour {

	public SceneInteractive sceneInteractive;

	public bool HasGetWorkPermit { get; set; }
	public bool HasTouchNewspaper { get; set; }
	public bool HasAlertHappen { get; set; }

	[SerializeField]
	private Image ObjectDrawer;
	[SerializeField]
	private Sprite DrawerClose;
	[SerializeField]
	private Sprite DrawerOpenItem;
	[SerializeField]
	private Sprite DrawerOpen;

	public AudioSource AlertLoop;
	public GameObject AlertLight;

	private bool _drawerItem = false;

	// Update is called once per frame
	void Update () {
		if (HasGetWorkPermit && HasTouchNewspaper && !HasAlertHappen && sceneInteractive.inIdle) {
			HasAlertHappen = true;
			Invoke ("AreaAlert", 1.5f);
		}
	}

	public void AreaAlert(){
		sceneInteractive.ShowTextOverlay ("溫度過高，請立即關閉反應爐！");
		Handheld.Vibrate ();
		AlertLight.SetActive (true);
		AlertLoop.Play ();
	}

	public void GetWorkPermit(){
		HasGetWorkPermit = true;
	}

	public void AlertCondition(){
		sceneInteractive.SetConditionFlagTo (HasAlertHappen);
	}

	public void DoDrawerOpen(){
		if (!_drawerItem) {
			ObjectDrawer.sprite = DrawerOpenItem;
		}
	}

	public void TakeDrawerItem(){
		ObjectDrawer.sprite = DrawerOpen;
	}
}
