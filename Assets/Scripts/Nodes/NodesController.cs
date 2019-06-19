using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesController : MonoBehaviour
{
	[Header("Color Data")]
	public Color startCol;
	public Color otherCol;
	public Color endColor;
	[Header("Node Collection")]
	public Nodes[] nodes;
	[Header("Common Node Properties")]
	public float ConnectorWidth = 0.17f;
	public Material ConnectionMaterial;


	public void FindAllNodes()
	{
		nodes = GameObject.FindObjectsOfType<Nodes>() as Nodes[];
		if (nodes.Length == 0)
			nodes = transform.parent.GetComponentsInChildren<Nodes>();
		if (nodes.Length == 0)
			nodes = GetComponentsInChildren<Nodes>();
		if (nodes.Length == 0)
			Debug.LogError("Take a manual check here");
	}

	//called by pressing the Inspector Function
	public void CreateLinks()
	{
		FindAllNodes();

		for (int index = 0; index < nodes.Length; index++)
		{
			//Common Node Properties
			nodes[index].ConfigureCommonData(ConnectorWidth, ConnectionMaterial);

			//Configure Individual Node
			nodes[index].ConfigureData();
			if (nodes[index].CurrentState == NodeState.Start)
			{
				nodes[index].myRenderer.color = startCol;
			}
			else if (nodes[index].CurrentState == NodeState.other)
			{
				nodes[index].myRenderer.color = otherCol;
			}
			else
			{
				nodes[index].myRenderer.color = endColor;
			}
		}
	}

	public void DeleteAllLinks()
	{
		FindAllNodes();

		for (int i=0;i<nodes.Length;i++)
		{
			nodes[i].RemoveOldLineRendererCnnections();
		}
	}
}
