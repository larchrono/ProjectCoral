using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DisplayObjectName : MonoBehaviour {
	
	[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
	static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
	{   
		GUIStyle style = new GUIStyle(); 
		style.normal.textColor = Color.red; 

		//Handles.Label(transform.position, transform.gameObject.name, style);

		//Handles.Label(transform.position, transform.gameObject.name);
	}

}
