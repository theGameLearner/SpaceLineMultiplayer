using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelScript))]
public class LevelScriptEditor : Editor
{
	public override void OnInspectorGUI()
	{
		LevelScript myTarget = (LevelScript)target;

		myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
		EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
		if (GUILayout.Button("Build Object"))
		{
			myTarget.TestingInspectorButton();
		}
	}
}