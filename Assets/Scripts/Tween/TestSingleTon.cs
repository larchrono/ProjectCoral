using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingleTon : MonoBehaviour
{

    private void Start() {
        throw new System.NotImplementedException();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            Debug.Log(TestRepeatRegist.main.valueForThis);
        }
    }
}
