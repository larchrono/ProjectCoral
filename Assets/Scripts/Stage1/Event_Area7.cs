using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Area7 : MonoBehaviour {

	public AreaClass Area_6;
	public AudioSource ErrorStep;
	public AudioSource BGM_Alert;
	public AudioSource BGM_Room;

	[Space(10)]

	public GameObject UserPoint;
	public GameObject StartPosition;
	public GameObject NowPosition;
	public GameObject GoalPosition;

	public GameObject LineCollection;
	public GameObject LightCollection;

	[Space(10)]

	public GameObject Prefabs_CurrentLine_H;
	public GameObject Prefabs_CurrentLine_V;
	public GameObject Prefabs_ErrorLine_H;
	public GameObject Prefabs_ErrorLine_V;

	[Space(10)]
	public bool CanControl;
	public GameObject Prefabs_PuzzleClear_Light;
	public GameObject Prefabs_CorrectLight;
	public GameObject Prefabs_ErrorLight;

	List<GameObject> RouteLineCollection;
	List<GameObject> HistoryLineCollection;

	List<GameObject> Lights;
	List<GameObject> HistoryLights;

	public enum Line_Direct {
		Horizen,
		Vertical
	}

	// Use this for initialization
	void Start () {
		RouteLineCollection = new List<GameObject> ();
		HistoryLineCollection = new List<GameObject> ();
		Lights = new List<GameObject> ();
		HistoryLights = new List<GameObject> ();
		CanControl = true;
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
	}

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
		PuzzleRouteNode nodeData = dist.GetComponent<PuzzleRouteNode> ();
		CreateRouteLine (dist, dir);
		if (nodeData.DegreeNumber == 0 || (nodeData.DegreeNumber > 21 && nodeData.DegreeNumber < 29)) {
			UserPoint.GetComponent<RectTransform> ().anchoredPosition = dist.anchoredPosition;
			NowPosition = dist.gameObject;
			if (nodeData.DegreeNumber != 0)
				Lighted (dist, false);
			CheckGoal (dist);
		} else {
			// Go to Error Route
			foreach (GameObject go in HistoryLineCollection) {
				Destroy (go);
			}
			HistoryLineCollection = new List<GameObject> (RouteLineCollection);
			foreach (GameObject go in HistoryLineCollection) {
				go.GetComponent<RouteLineObject> ().ConvertToError ();
			}
			// Reset User
			RouteLineCollection.Clear ();
			UserPoint.GetComponent<RectTransform> ().anchoredPosition = StartPosition.GetComponent<RectTransform>().anchoredPosition;
			NowPosition = StartPosition;

			Lighted (dist, true);

			foreach (GameObject go in HistoryLights) {
				Destroy (go);
			}
			HistoryLights = new List<GameObject> (Lights);
			Lights.Clear ();

			ErrorStep.Play ();
		}
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

	public void CheckGoal(RectTransform dist){
		if (dist.gameObject == GoalPosition) {
			Prefabs_PuzzleClear_Light.SetActive (true);
			UserPoint.SetActive (false);
			CanControl = false;
			Area_6.GetComponent<Event_Area6> ().UnlockGoBack ();

			BGM_Alert.Stop ();
			BGM_Room.Play ();
		}
	}

	public void Lighted(RectTransform dist, bool err){
		GameObject temp;
		if(err == false)
			temp = Instantiate (Prefabs_CorrectLight, LightCollection.transform);
		else 
			temp = Instantiate (Prefabs_ErrorLight, LightCollection.transform);
		
		temp.GetComponent<RectTransform> ().anchoredPosition = dist.anchoredPosition;
		Lights.Add (temp);
	}
}
