using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaClass : MonoBehaviour {
    
    public QueueAction AreaInit;
    public QueueAction AreaActive;

    void Start(){
        AreaInit.Invoke();
    }

    void OnEnable() {
        AreaActive.Invoke();
    }
}
