using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagTitle : MonoBehaviour {

	public Text ShowTextTitle;
	public GameObject BagPanel;

	public void SwitchBagPanel(){
		if (BagPanel.activeInHierarchy) {
			BagPanel.SetActive (false);
			ShowTextTitle.text = "< 持有物品";
		} else {
			BagPanel.SetActive (true);
			ShowTextTitle.text = "> 持有物品";
		}
	}

	public void CloseBag(){
		BagPanel.SetActive (false);
		ShowTextTitle.text = "< 持有物品";
	}

	public void OpenBag(){
		BagPanel.SetActive (true);
		ShowTextTitle.text = "> 持有物品";
	}
}
