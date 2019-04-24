using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Event_Area7_Type2 : MonoBehaviour
{
    //Public 
    public enum Line_Direct {
		Horizen,
		Vertical
	}

    public GameObject Prefab_NodeLight;

    public bool CanControl;
    public PuzzleRouteNode StartPosition;
    public PuzzleRouteNode NowPosition;
	public GameObject Object_StartLight;
    public GameObject Object_CollectionLight;
    public List<GameObject> List_HistoryNodeStep;
    public List<GameObject> List_CollectionLight;


    //Private


    //Callback
    [SerializeField]
    public QueueAction WhenErrorStep;

    [SerializeField]
    public QueueAction WhenGotoGoal;

	[SerializeField]
    public QueueAction WhenAfterGoal;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
			MoveLeft ();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			MoveRight ();
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			MoveUp ();
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			MoveDown ();
		}
    }


    	//檢查要過去的位置有沒有節點，確認之後過去
	public void MoveLeft(){
		if (!CanControl)
			return;
		if (NowPosition.NodeLeft != null) {
			RectTransform TargetTransform = NowPosition.NodeLeft.GetComponent<RectTransform> ();

			MoveTo (TargetTransform);
		}
	}
	public void MoveRight(){
		if (!CanControl)
			return;
		if (NowPosition.NodeRight != null) {
			RectTransform TargetTransform = NowPosition.NodeRight.GetComponent<RectTransform> ();

			MoveTo (TargetTransform);
		}
	}
	public void MoveUp(){
		if (!CanControl)
			return;
		if (NowPosition.NodeUp != null) {
			RectTransform TargetTransform = NowPosition.NodeUp.GetComponent<RectTransform> ();

			MoveTo (TargetTransform);
		}
	}
	public void MoveDown(){
		if (!CanControl)
			return;
		if (NowPosition.NodeDown != null) {
			RectTransform TargetTransform = NowPosition.NodeDown.GetComponent<RectTransform> ();

			MoveTo (TargetTransform);
		}
	}

    public void MoveTo(RectTransform dist){
        //Step1. 不可往回走
        if(List_HistoryNodeStep.Contains(dist.gameObject))
            return;
		
		//Step1. 設置起始燈恆亮
		if(List_HistoryNodeStep.Count == 0){
			Object_StartLight.GetComponent<DOTweenAnimation>().DORestart();
			Object_StartLight.GetComponent<DOTweenAnimation>().DOPause();
		}

        //Step2. 取得目標的Class資訊
		PuzzleRouteNode nodeData = dist.GetComponent<PuzzleRouteNode> ();

        if ((nodeData.DegreeNumber > 21 && nodeData.DegreeNumber < 29)) {
            LightedDegreePoint(dist,true);

            List_HistoryNodeStep.Add(NowPosition.gameObject);
            NowPosition = dist.gameObject.GetComponent<PuzzleRouteNode>();

			CheckNodeGoal(NowPosition);
            
        } else {
            LightedDegreePoint(dist,false);

            List_HistoryNodeStep.Clear();
            NowPosition = StartPosition;

			Handheld.Vibrate ();
            WhenErrorStep.Invoke();
            StartCoroutine(ClearAllLight());
        }
    }

    public void LightedDegreePoint(RectTransform dist, bool currect){
		GameObject temp = Instantiate (Prefab_NodeLight, Object_CollectionLight.transform);
        temp.GetComponent<RectTransform> ().anchoredPosition = dist.anchoredPosition;

		if(currect)
			temp.GetComponent<UsedSpritePool>().SetSpriteToPoolID(0);
		else 
			temp.GetComponent<UsedSpritePool>().SetSpriteToPoolID(1);
		
		List_CollectionLight.Add (temp);
	}

    public IEnumerator ClearAllLight(){
		CanControl = false;
        yield return new WaitForSeconds(1.0f);

        foreach (GameObject go in List_CollectionLight) {
            Destroy (go);
        }
        List_CollectionLight.Clear ();
		Object_StartLight.GetComponent<DOTweenAnimation>().DOPlay();
		CanControl = true;
    }

	void CheckNodeGoal(PuzzleRouteNode node){
		if(node.isGoal){
			WhenGotoGoal.Invoke();
		}
	}

	public void SetCanControl(bool setting){
		CanControl = setting;
	}

	public void FinishSequence(){
		StartCoroutine(SoundBreak());
	}

	IEnumerator SoundBreak(){
		yield return new WaitForSeconds(1.0f);
		WhenAfterGoal.Invoke();
	}
}
