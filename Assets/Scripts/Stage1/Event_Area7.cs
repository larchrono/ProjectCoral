using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Area7 : MonoBehaviour {

	public AreaClass Area_4;
	public AudioSource ErrorStep;
	public AudioSource BGM_Alert;
	public AudioSource BGM_Room;

	[Space(10)]

	public GameObject UserPoint;
	public GameObject StartPosition;
	public GameObject NowPosition;
	public GameObject GoalPosition;

	public GameObject LineCollection;
	public GameObject SpotCollection;
	public GameObject LightCollection;

	[Space(10)]

	public GameObject Prefabs_CurrentLine_H;
	public GameObject Prefabs_CurrentLine_V;
	public GameObject Prefabs_ErrorLine_H;
	public GameObject Prefabs_ErrorLine_V;
	public GameObject Prefabs_Spot;
	public Sprite Sprite_RedSpot;

	[Space(10)]
	public bool CanControl;
	public Image PuzzleCenter_Light;
	public GameObject Prefabs_CorrectLight;
	public GameObject Prefabs_ErrorLight;

	[SerializeField]
	List<GameObject> RouteLineCollection;
	List<GameObject> HistoryLineCollection;

	[SerializeField]
	List<GameObject> CollectionSpot;
	List<GameObject> CollectionHistorySpot;

	List<GameObject> NowLights;
	List<GameObject> HistoryLights;

	[SerializeField]
	List<GameObject> HistoryPosition;

	public enum Line_Direct {
		Horizen,
		Vertical
	}

	// Use this for initialization
	void Start () {
		RouteLineCollection = new List<GameObject> ();
		HistoryLineCollection = new List<GameObject> ();
		NowLights = new List<GameObject> ();
		HistoryLights = new List<GameObject> ();
		HistoryPosition = new List<GameObject> ();
		CollectionSpot = new List<GameObject> ();
		CollectionHistorySpot = new List<GameObject> ();
		CanControl = true;

		HistoryPosition.Add (StartPosition);
	}
	
	// Update is called once per frame
	void Update () {
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
		if(Input.GetKeyDown(KeyCode.Space)){
			ResetStep ();
		}
	}

	//檢查要過去的位置有沒有節點，確認之後過去
	public void MoveLeft(){
		if (!CanControl)
			return;
		if (NowPosition.GetComponent<PuzzleRouteNode> ().NodeLeft != null) {
			RectTransform TargetTransform = NowPosition.GetComponent<PuzzleRouteNode> ().NodeLeft.GetComponent<RectTransform> ();

			MoveTo (TargetTransform,Line_Direct.Horizen);
		}
	}
	public void MoveRight(){
		if (!CanControl)
			return;
		if (NowPosition.GetComponent<PuzzleRouteNode> ().NodeRight != null) {
			RectTransform TargetTransform = NowPosition.GetComponent<PuzzleRouteNode> ().NodeRight.GetComponent<RectTransform> ();

			MoveTo (TargetTransform,Line_Direct.Horizen);
		}
	}
	public void MoveUp(){
		if (!CanControl)
			return;
		if (NowPosition.GetComponent<PuzzleRouteNode> ().NodeUp != null) {
			RectTransform TargetTransform = NowPosition.GetComponent<PuzzleRouteNode> ().NodeUp.GetComponent<RectTransform> ();

			MoveTo (TargetTransform,Line_Direct.Vertical);
		}
	}
	public void MoveDown(){
		if (!CanControl)
			return;
		if (NowPosition.GetComponent<PuzzleRouteNode> ().NodeDown != null) {
			RectTransform TargetTransform = NowPosition.GetComponent<PuzzleRouteNode> ().NodeDown.GetComponent<RectTransform> ();

			MoveTo (TargetTransform,Line_Direct.Vertical);
		}
	}



	public void MoveTo(RectTransform dist,Line_Direct dir){
		//Step1. 取得目標的Class資訊
		PuzzleRouteNode nodeData = dist.GetComponent<PuzzleRouteNode> ();
		//Step2. 建立路徑圖
		CreateRouteLine (dist, dir);
		//Step3. 建立Spot
		CreateSpot(dist);

		//Step4. 紀錄到過的position
		UpdatePositionStack(dist);


		//Step5. 溫度數字資訊來阻擋移動
		if (nodeData.DegreeNumber == 0 || (nodeData.DegreeNumber > 21 && nodeData.DegreeNumber < 29)) {
			UserPoint.GetComponent<RectTransform> ().anchoredPosition = dist.anchoredPosition;
			NowPosition = dist.gameObject;
			if (nodeData.DegreeNumber != 0)
				LightedDegreePoint (dist, false);
			CheckGoal (dist);
		} else {
			// Go to Error Route
			foreach (GameObject go in HistoryLineCollection) {
				Destroy (go);
			} HistoryLineCollection = new List<GameObject> (RouteLineCollection);
			RouteLineCollection.Clear ();
			foreach (GameObject go in HistoryLineCollection) {
				go.GetComponent<RouteLineObject> ().ConvertToError ();
			}

			// Spot To History and clear
			foreach (GameObject go in CollectionHistorySpot) {
				Destroy (go);
			} CollectionHistorySpot = new List<GameObject> (CollectionSpot);
			CollectionSpot.Clear ();
			foreach (GameObject go in CollectionHistorySpot) {
				go.GetComponent<Image> ().sprite = Sprite_RedSpot;
			}


			// Reset User
			UserPoint.GetComponent<RectTransform> ().anchoredPosition = StartPosition.GetComponent<RectTransform>().anchoredPosition;
			NowPosition = StartPosition;

			LightedDegreePoint (dist, true);

			foreach (GameObject go in HistoryLights) {
				Destroy (go);
			}
			HistoryLights = new List<GameObject> (NowLights);
			NowLights.Clear ();

			//清空HistoryPosition
			HistoryPosition.Clear();
			HistoryPosition.Add (StartPosition);

			ErrorStep.Play ();
		}
	}

	public void ResetStep(){
		if (!CanControl)
			return;
		
		// Reset HistoryLineCollection
		foreach (GameObject go in HistoryLineCollection) {
			Destroy (go);
		} HistoryLineCollection.Clear ();

		// Reset RouteLineCollection
		foreach (GameObject go in RouteLineCollection) {
			Destroy (go);
		} RouteLineCollection.Clear ();

		// Reset History Spot
		foreach (GameObject go in CollectionHistorySpot) {
			Destroy (go);
		} CollectionHistorySpot.Clear ();

		// Reset Now Spot
		foreach (GameObject go in CollectionSpot) {
			Destroy (go);
		} CollectionSpot.Clear ();

		// Reset User
		UserPoint.GetComponent<RectTransform> ().anchoredPosition = StartPosition.GetComponent<RectTransform>().anchoredPosition;
		NowPosition = StartPosition;

		// Reset History Light
		foreach (GameObject go in HistoryLights) {
			Destroy (go);
		} HistoryLights.Clear ();

		// Reset Now Light
		foreach (GameObject go in NowLights) {
			Destroy (go);
		} NowLights.Clear ();

		//清空HistoryPosition
		HistoryPosition.Clear();
		HistoryPosition.Add (StartPosition);

	}

	public void CreateRouteLine(RectTransform dist,Line_Direct dir){
		GameObject prefab;
		if (dir == Line_Direct.Horizen)
			prefab = Prefabs_CurrentLine_H;
		else
			prefab = Prefabs_CurrentLine_V;
		
		Vector2 pos = (NowPosition.GetComponent<RectTransform> ().anchoredPosition + dist.anchoredPosition) / 2;
		GameObject line = Instantiate (prefab, LineCollection.transform);
		line.GetComponent<RectTransform> ().anchoredPosition = pos;
		RouteLineCollection.Add (line);
	}

	void CreateSpot(RectTransform dist){
		Vector2 pos = dist.anchoredPosition;
		GameObject spot = Instantiate (Prefabs_Spot, SpotCollection.transform);
		spot.GetComponent<RectTransform> ().anchoredPosition = pos;
		CollectionSpot.Add (spot);
	}

	void PullRouteLine(){
		GameObject temp = RouteLineCollection[RouteLineCollection.Count - 1];
		Destroy (temp);
		RouteLineCollection.RemoveAt (RouteLineCollection.Count-1);
	}

	void PullRouteSpot(){
		GameObject temp = CollectionSpot[CollectionSpot.Count - 1];
		Destroy (temp);
		CollectionSpot.RemoveAt (CollectionSpot.Count-1);
	}

	void UpdatePositionStack(RectTransform pos){
		if (HistoryPosition.Contains (pos.gameObject)) {
			//Remove route now craete
			PullRouteLine ();
			PullRouteSpot ();
			//Remove route last create
			PullRouteLine ();
			PullRouteSpot ();

			HistoryPosition.RemoveAt (HistoryPosition.Count - 1);
		} else {
			HistoryPosition.Add (pos.gameObject);
		}
	}

	//Area Clear 
	public void CheckGoal(RectTransform dist){
		if (dist.gameObject == GoalPosition) {
			Sprite _greenLight = PuzzleCenter_Light.GetComponent<UsedSpritePool>().UsedSprite[1];
			PuzzleCenter_Light.sprite = _greenLight;
			
			UserPoint.SetActive (false);
			CanControl = false;
			
			Area_4.GetComponent<Event_Area4> ().OnArea7_Clear.Invoke();

			BGM_Alert.Stop ();
			BGM_Room.Play ();
		}
	}

	public void LightedDegreePoint(RectTransform dist, bool err){
		GameObject temp;
		if(err == false)
			temp = Instantiate (Prefabs_CorrectLight, LightCollection.transform);
		else 
			temp = Instantiate (Prefabs_ErrorLight, LightCollection.transform);
		
		temp.GetComponent<RectTransform> ().anchoredPosition = dist.anchoredPosition;
		NowLights.Add (temp);
	}
}
