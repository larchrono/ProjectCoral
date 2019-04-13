using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexSystems : MonoBehaviour
{

    [SerializeField]
    Toggle Toggle_IsShowTouch;

    void Awake()
    {
        Toggle_IsShowTouch.isOn = GlobalVariables.instance.isDebugArea;

		bool useDebugArea = PlayerPrefs.GetInt("DebugArea", 0) > 0 ? true : false;
		bool useSound = PlayerPrefs.GetInt("UseSound", 1) > 0 ? true : false;

		GlobalVariables.instance.SetUseSound(useDebugArea);
		GlobalVariables.instance.SetUseSound(useSound);

        Debug.Log("Reading PlayerPrefs DebugArea: " + useDebugArea);
        Debug.Log("Reading PlayerPrefs UseSound: " + useSound);
    }

    public void SetShowTouchAreaColor(bool src)
    {
        GlobalVariables.instance.SetDebugArea(src);
    }


}
