using liusuwanxia.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xiaTimerTest : MonoBehaviour {
	public Text textTimer;

	private IEnumerator coroutine;
	private int count = 60;

	// Use this for initialization
	void Start () {
		coroutine = Timer.Start(0.5f, true, () => {
			if (count <= 0) StopCoroutine(coroutine);
			textTimer.text = string.Format("Timer: {0}", count--);
		});

	}

	public void OnBtnStartClick()
	{
		StartCoroutine(coroutine);
	}

	public void OnBtnStopClick()
	{
		StopCoroutine(coroutine);
	}
}