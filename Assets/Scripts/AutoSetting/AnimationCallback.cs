using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallback : MonoBehaviour
{
    public QueueAction OnStateEnd;

    void _OnStateEnd(){
        OnStateEnd.Invoke();
        Debug.Log("Animation End");
    }

}
