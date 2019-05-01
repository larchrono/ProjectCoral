using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCoral : MonoBehaviour
{
    public int nowStat;
    private bool swaping = false;
    Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }

    public void AnimFin(){
        swaping = false;
    }

    public void PlayAndSwitchState(){
        if(swaping)
            return;

        if(nowStat == 0){
            nowStat = 1;
            anim.SetTrigger("Go");
            swaping = true;
        } else {
            nowStat = 0;
            anim.SetTrigger("Back");
            swaping = true;
        }
    }
}
