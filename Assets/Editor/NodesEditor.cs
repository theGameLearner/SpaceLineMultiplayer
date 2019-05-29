using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Nodes))]
public class NodesEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Nodes myTarget = (Nodes)target;
		EditorGUILayout.LabelField("Previous Node", myTarget.prevNode == null ? "NA" : myTarget.prevNode.name);
		DrawDefaultInspector();
		EditorGUILayout.LabelField("List Count", myTarget.myLocalConnections.Count.ToString());

		if (GUILayout.Button("Set My Node"))
		{
			myTarget.ConfigureData();
		}

		if(myTarget.myLocalConnections.Count > 0)
		{
			if(GUILayout.Button("ReConfigure Links"))
			{
				myTarget.UpdatedNodePositions();
			}

			if(GUILayout.Button("Delete Links"))
			{
				myTarget.RemoveOldLineRendererCnnections();
			}
		}
	}
}
