using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRepeatRegist : MonoBehaviour
{
    event EventHandler testEvent;
    // Start is called before the first frame update
    void Start()
    {
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
        testEvent += TestReg;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            testEvent(this,new EventArgs());
        }
        if(Input.GetKeyDown(KeyCode.D)){
            testEvent -= TestReg;
            
        }
    }

    void TestReg(object sender,EventArgs args){
        Debug.Log("Call!");
    }
}
