using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnActive : MonoBehaviour
{
    public QueueAction whenActive;

    const float _delay = 0.1f;
    void OnEnable() {
        StartCoroutine(DelayAction());
    }

    IEnumerator DelayAction(){
        yield return new WaitForSeconds(_delay);
        whenActive.Invoke();
    }
}
