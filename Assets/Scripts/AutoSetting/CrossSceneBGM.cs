using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneBGM : MonoBehaviour
{
    public static CrossSceneBGM instance;

    void Awake(){
        if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
    }
}
