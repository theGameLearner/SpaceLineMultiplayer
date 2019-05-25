using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
	public int experience;

	public int Level
	{
		get { return experience / 750; }
	}

	public void TestingInspectorButton()
	{
		Debug.Log("Printed by Inspector Button");
		Debug.LogWarning("Printed by Inspector Button");
		Debug.LogError("Printed by Inspector Button");
	}
}
