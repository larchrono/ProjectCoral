using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBack : MonoBehaviour
{
    public Image Image_UI;

    bool _forceCommand = false;

    // Update is called once per frame
    void Update()
    {
        if(_forceCommand)
            return;
            
        if(SceneStack.main.AreaStack.Count <= 1){
            Image_UI.raycastTarget = false;
            Image_UI.color = new Color(1,1,1,0);
        } else {
            Image_UI.raycastTarget = true;
            Image_UI.color = new Color(1,1,1,1);
        }
    }

    public void HideButtonBack(){
        Image_UI.raycastTarget = false;
        Image_UI.color = new Color(1,1,1,0);
        _forceCommand = true;
    }

    public void ShowButtonBack(){
        Image_UI.raycastTarget = true;
        Image_UI.color = new Color(1,1,1,1);
        _forceCommand = false;
    }
}
