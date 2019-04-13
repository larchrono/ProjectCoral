using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Area6 : MonoBehaviour {

	public SceneInteractive sceneInteractive;
	public AreaClass Area_8;

	public Button GoBackObject;

	public Image Center_Light;

	public void UnlockGoBack(){
		GoBackObject.onClick.RemoveAllListeners ();
		GoBackObject.onClick.AddListener (() => sceneInteractive.GoToArea (Area_8));
		Sprite _greenLight = Center_Light.GetComponent<UsedSpritePool>().UsedSprite[1];
		Center_Light.sprite = _greenLight;
	}
}
