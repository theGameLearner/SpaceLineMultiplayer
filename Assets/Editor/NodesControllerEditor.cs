using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodesController))]
public class NodesControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		NodesController myTarget = (NodesController)target;

		if (GUILayout.Button("Set Nodes"))
		{
			myTarget.CreateLinks();
		}
	}
}
