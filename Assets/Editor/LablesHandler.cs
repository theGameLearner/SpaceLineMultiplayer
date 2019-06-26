using UnityEngine;
using UnityEditor;

public class LablesHandler : Editor
{
	[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
	static void DrawGameObjectName(Transform objTransform, GizmoType gizmoType)
	{
		if (objTransform.GetComponent<Nodes>())
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red;
			style.fontSize = 15;
			Handles.Label(objTransform.position, objTransform.gameObject.name, style);
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