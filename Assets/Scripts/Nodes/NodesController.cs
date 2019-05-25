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


	//called by pressing the Inspector Function
	public void CreateLinks()
	{
		
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
}
