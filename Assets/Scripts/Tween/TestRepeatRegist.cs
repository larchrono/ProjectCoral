using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRepeatRegist : MonoBehaviour
{
    event EventHandler testEvent;
    // Start is called before the first frame update

    public static TestRepeatRegist main;

    public int valueForThis;

    public TestRepeatRegist(){
        main = this;
        Debug.Log("Contructor called ! ");
    }

    void Awake() {
        Debug.Log("Awake regardless script enable");
    }

    void Start()
    {
        Debug.Log("This is Start , will be trigger once");
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

    private void OnEnable() {
        Debug.Log("This is OnEnable , will be trigger when Set Active");
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
