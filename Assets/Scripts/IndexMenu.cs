using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndexMenu : MonoBehaviour {

	public void LoadScene(int stage){
		SceneManager.LoadScene ("Scene" + stage);
	}

	public void LoadScene(string stage){
		SceneManager.LoadScene (stage);
	}

	public void LoadSceneDelay(int stage, float delay){
		StartCoroutine (IELoadSceneDelay (stage, delay));
	}

	public void LoadSceneDelay(string stage, float delay){
		StartCoroutine (IELoadSceneDelay (stage, delay));
	}

	IEnumerator IELoadSceneDelay(int stage, float delay){
		yield return new WaitForSeconds (delay);
		LoadScene (stage);
	}

	IEnumerator IELoadSceneDelay(string stage, float delay){
		yield return new WaitForSeconds (delay);
		LoadScene (stage);
	}

	//////extend //////

	public void BackToMenu(){
		LoadSceneDelay ("Index", 3f);
	}
}
