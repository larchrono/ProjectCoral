using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Stage2_Area2_Bottle : MonoBehaviour {

	[SerializeField]
	SceneInteractive sceneInteractive = null;
	[SerializeField]
	Image Image_Background = null;
	[SerializeField]
	Sprite[] Sprite_PutSteps = null;

	[Space(10)]
	[SerializeField]
	Button Button_Undone = null;
	[SerializeField]
	Button[] Buttons_CanPutItem = null;
	[Space(10)]
	[SerializeField]
	Button LeaveStage = null;
	[SerializeField]
	Image Image_OutsideBackground = null;
	[SerializeField]
	Sprite Sprite_Outside_Finish = null;

	void OnEnable()
	{
		return;

		if (!sceneInteractive.CheckBagHasItem ("怪手"))
			return;
		if (!sceneInteractive.CheckBagHasItem ("小樹苗"))
			return;
		if (!sceneInteractive.CheckBagHasItem ("木雕魚"))
			return;
		if (!sceneInteractive.CheckBagHasItem ("網子"))
			return;

		Button_Undone.gameObject.SetActive (false);
		Buttons_CanPutItem[0].gameObject.SetActive (true);
	}

	public void PlaySoundCondition(AudioSource snd){
		if (!sceneInteractive.GetItemUseResult ())
			return;

		snd.Play ();
	}

	public void PutItem(int step){
		if (!sceneInteractive.GetItemUseResult ()){
			SceneInteractive.main.ShowTextOverlay("葉子、魚、網子、挖土機？");
			return;
		}
		
		Image_Background.sprite = Sprite_PutSteps[step];
		Buttons_CanPutItem[step].gameObject.SetActive (false);
		if( step+1 < Buttons_CanPutItem.Length)
			Buttons_CanPutItem[step+1].gameObject.SetActive (true);
	}

	public void FinishPitting(){
		if (!sceneInteractive.GetItemUseResult ())
			return;
		
		sceneInteractive.ShowTextOverlay ("發生劇烈搖晃！");
		Handheld.Vibrate ();

		LeaveStage.gameObject.SetActive (true);

		Image_OutsideBackground.sprite = Sprite_Outside_Finish;
	}
}
