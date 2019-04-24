using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBack : MonoBehaviour
{
    public Image Image_UI;

    // Update is called once per frame
    void Update()
    {
        if(SceneStack.main.AreaStack.Count <= 1){
            Image_UI.raycastTarget = false;
            Image_UI.color = new Color(1,1,1,0);
        } else {
            Image_UI.raycastTarget = true;
            Image_UI.color = new Color(1,1,1,1);
        }
    }
}
