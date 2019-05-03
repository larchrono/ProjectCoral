using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayDebug : MonoBehaviour
{
    public static OverlayDebug main;

    public Text Text_Show;

    int MaxStringLine = 10;
    //List<string> stockString;
    Queue<string> stockString;

    void Awake(){
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stockString = new Queue<string>();
        if(GlobalVariables.instance.isDebugArea){
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    public void QueueString(string src){
        if(stockString == null)
            stockString = new Queue<string>();
        stockString.Enqueue(src);
        if(stockString.Count > MaxStringLine){
            stockString.Dequeue();
        }
        if(Text_Show == null)
            return;
        Text_Show.text = "";
        foreach (var item in stockString)
        {
            Text_Show.text += item + "\n";
        }
    }
}
