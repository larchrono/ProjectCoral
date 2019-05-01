using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStack : MonoBehaviour {
	public static SceneStack main;

	// in MonoBehaviour , this variable (public) will be alloc automatic
	public List<AreaClass> AreaStack;

	void Awake(){
		main = this;
	}

	public AreaClass GetLastAreaStack() {
		int cnt = AreaStack.Count;
		if(cnt > 0)
			return AreaStack [cnt - 1];

		return null;
	}

	public void PushAreaStack(AreaClass area){
		AreaStack.Add (area);
	}

	public AreaClass PullAreaStack(){
		int cnt = AreaStack.Count;
		if (cnt > 1) {
			AreaClass targetArea = AreaStack [cnt - 1];
			AreaStack.RemoveAt (cnt - 1);
			return targetArea;
		}

		return null;
	}

	public void InsteadAreaStack(AreaClass area){
		int cnt = AreaStack.Count;
		if (cnt > 0) {
			AreaClass targetArea = AreaStack [cnt - 1];
			AreaStack.RemoveAt (cnt - 1);
		}
		AreaStack.Add (area);
	}

	public void RemoveAreaFromStack(AreaClass area){
		int index = AreaStack.FindIndex(x => x == area);
		Debug.Log("find to remove index :" + index);
		if(index != -1){
			AreaStack.RemoveAt(index);
		}
	}
}
