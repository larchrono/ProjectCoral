using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStack : MonoBehaviour {

	// in MonoBehaviour , this variable (public) will be alloc automatic
	public List<AreaClass> AreaStack;

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
		if (cnt > 0) {
			AreaClass targetArea = AreaStack [cnt - 1];
			AreaStack.RemoveAt (cnt - 1);
			return targetArea;
		}

		return null;
	}
}
