using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodesController))]
public class NodesControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		NodesController myTarget = (NodesController)target;

		if(GUILayout.Button("Find Nodes"))
		{
			myTarget.FindAllNodes();
		}

		if (GUILayout.Button("Set Nodes"))
		{
			myTarget.CreateLinks();
		}

		if (GUILayout.Button("Delete all Links"))
		{
			myTarget.DeleteAllLinks();
		}

		if(GUILayout.Button("Populate Nodes Speed List"))
		{
			myTarget.PopulateAllNodesSpeedList();
		}
	}
}
