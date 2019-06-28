using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Nodes))]
public class NodesEditor : Editor
{
	Nodes myTarget;
	public override void OnInspectorGUI()
	{

		if (myTarget == null)
		{
			myTarget = (Nodes)target;
		}

		//label to show previous node. (might remove if we do not feel the need)
		EditorGUILayout.LabelField("Previous Node", myTarget.prevNode == null ? "NA" : myTarget.prevNode.name);
		//default inspector view for script
		DrawDefaultInspector();
		//count of connections starting from this node
		EditorGUILayout.LabelField("List Count", myTarget.myLocalConnections.Count.ToString());

		//Set the node properly
		if (GUILayout.Button("Set My Node"))
		{
			myTarget.ConfigureData();
		}

		//only show if the current nodes has a few destination nodes set up
		if(myTarget.myLocalConnections.Count > 0)
		{
			//verify all links are proper
			if(GUILayout.Button("ReConfigure Links"))
			{
				myTarget.UpdatedNodePositions();
			}

			//delete all links.
			if(GUILayout.Button("Delete Links"))
			{
				myTarget.RemoveOldLineRendererCnnections();
			}
		}

		//edit speed list to make all path speed as 3
		if(GUILayout.Button("Add Speed List"))
		{
			myTarget.PopulateSpeedForDest();
		}

		
	}

	
	
}
