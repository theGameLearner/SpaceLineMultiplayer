using UnityEngine;
using UnityEditor;

public class LablesHandler : Editor
{
	[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
	static void DrawGameObjectName(Transform objTransform, GizmoType gizmoType)
	{
		if (objTransform.GetComponent<Nodes>())
		{
			Nodes node = objTransform.GetComponent<Nodes>();
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red;
			style.fontSize = 15;
			Handles.Label(objTransform.position, objTransform.gameObject.name, style);
			style.normal.textColor = Color.yellow;
			style.fontSize = 10;
			for(int i=0;i<node.myDestSpeed.Count;i++){
				Transform destTransform = node.myDestinations[i].transform;
				Vector3 pos = Vector3.Lerp(objTransform.position,destTransform.position,0.5f);
				Handles.Label(pos, "speed: "+node.myDestSpeed[i], style);

			}
		}
		else if (objTransform.GetComponent<coinControllerScript>() || objTransform.GetComponent<asteroidControllerScript>())
		{
			GUIStyle style = new GUIStyle();
			Handles.ArrowHandleCap(
									0,
									objTransform.position,
									objTransform.rotation * Quaternion.LookRotation(Vector3.right),
									1,
									EventType.Repaint
				);
		}

	}
}