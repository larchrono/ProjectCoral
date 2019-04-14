using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexSystems : MonoBehaviour
{

    [SerializeField]
    Toggle Toggle_IsShowTouch;

    void Start()
    {
        Toggle_IsShowTouch.isOn = GlobalVariables.instance.isDebugArea;
    }

    public void SetShowTouchAreaColor(bool src)
    {
        GlobalVariables.instance.SetDebugArea(src);
    }


}
