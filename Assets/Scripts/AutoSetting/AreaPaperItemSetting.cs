using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPaperItemSetting : MonoBehaviour
{
    public int paperID;
    // Start is called before the first frame update
    void Start()
    {
        int result = GlobalVariables.instance.PageItems[paperID];

        if(result > 0){
            gameObject.SetActive(false);
        }
    }

}
