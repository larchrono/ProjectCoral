using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_Stage : MonoBehaviour
{

    [Header("Auto Setting Sound via GlobalVariable")]

    [Space(10)]

    [SerializeField]
    public QueueAction willRunAction;

    [SerializeField]
    GameObject[] NeedInitOverlays = null;

    void Awake() {
        foreach (var item in NeedInitOverlays)
        {
            item.SetActive(true);
            item.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        SettingSoundOption();

        StartCoroutine(DoWillRunAction());
    }

    IEnumerator DoWillRunAction(){
        yield return null;
        willRunAction.Invoke();
    }

    public void SettingSoundOption()
    {
        bool _soundSetting = GlobalVariables.instance.isUseSound;
        AudioListener.volume = _soundSetting ? 1 : 0;
    }

}
