using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Nodes : MonoBehaviour
{

	public NodeState CurrentState;
	public bool start, end;
	//public Nodes forwardNode, leftNode, rightNode;
	public List<Nodes> myDestinations = new List<Nodes>();
	public List<float> myDestSpeed = new List<float>();
	[HideInInspector]
	public Nodes prevNode;
	public SpriteRenderer myRenderer;
	[HideInInspector]
	public List<LineRenderer> myLocalConnections = new List<LineRenderer>();
	[HideInInspector]
	public List<Nodes> PreviousNodes = new List<Nodes>();
	


	float ConnectorWidth = 0.17f;
	Material ConnectorMaterial;


	//set up some default values until the Controller calls for data
	void Reset()
	{
		if (ConnectorWidth == 0)
			ConnectorWidth = 0.17f;
		if (ConnectorMaterial == null)
		{
			if (FindObjectOfType<NodesController>().ConnectionMaterial)
			{
				ConnectorMaterial = FindObjectOfType<NodesController>().ConnectionMaterial;
			}
			else
			{
				ConnectorMaterial = new Material(Shader.Find("Sprites/Default"));
			}
		}
		myRenderer = GetComponent<SpriteRenderer>();
		SetState();
	}




	/// <summary>
	/// Configure the Node to work properly and have necessary references
	/// </summary>
	public void ConfigureData()
	{
		myRenderer = GetComponent<SpriteRenderer>();
		SetState();
		RemoveOldLineRendererCnnections();
		CreateNewLinks();
	}

	/// <summary>
	/// Configure all Data specified in Controller to individual nodes
	/// </summary>
	/// <param name="connWidth">With of all Connections</param>
	/// <param name="connectionMtl">Material to use for Line Renderers</param>
	public void ConfigureCommonData(float connWidth, Material connectionMtl)
	{
		ConnectorWidth = connWidth;
		ConnectorMaterial = connectionMtl;
	}

	/// <summary>
	/// Checks the Enum and Changes the bool. 
	/// Kept here in case we want to avoid enums later
	/// </summary>
	private void SetState()
	{
		if(CurrentState == NodeState.Start)
		{
			start = true;
			end = false;
		}
		else if(CurrentState == NodeState.End)
		{
			start = false;
			end = true;
		}
		else
		{
			start = false;
			end = false;
		}
	}

	/// <summary>
	/// Remove any Connections we had to forward moving nodes
	/// </summary>
	public void RemoveOldLineRendererCnnections()
	{
		//remove all Line Renderers
		LineRenderer[] myConnects = GetComponentsInChildren<LineRenderer>();
		if (myConnects.Length > 0)
		{
			for (int i = 0; i < myConnects.Length; i++)
			{
				myLocalConnections.Remove(myConnects[i]);
				DestroyImmediate(myConnects[i].gameObject);
			}
			myConnects = null;
		}
		//verify there is no additional connection we forgot to delete.
		if (myLocalConnections.Count > 0)
		{
			Debug.LogError("There is an error, in " + name + ", myLocalConnections still has " + myLocalConnections.Count + " connections");
			myLocalConnections.Clear();
		}
	}

	/// <summary>
	/// Check the Nodes Connected and create the necessary Connections
	/// </summary>
	private void CreateNewLinks()
	{
		for (int i = 0; i < myDestinations.Count; i++) 
		{
			CreateNewConnection("Line_" + i.ToString("00"), myDestinations[i].transform.position);
		}
	}

	/// <summary>
	/// Creates a new Connection Path. And names the Object as per 'LR_name' with
	/// the line renderers destination set as 'destPos'
	/// </summary>
	/// <param name="LR_name">The name to assign to Line Renderer's GameObject</param>
	/// <param name="destPos">The Destination of Line Renderer</param>
	void CreateNewConnection(string LR_name, Vector3 destPos)
	{
		GameObject gob_Object = new GameObject();
		gob_Object.transform.parent = transform;
		gob_Object.name = LR_name;
		gob_Object.AddComponent<LineRenderer>();

		//add a line renderer to this new Object
		LineRenderer visibleLine = gob_Object.GetComponent<LineRenderer>();

		//Set the start and End Point
		visibleLine.positionCount = 2;
		visibleLine.SetPosition(0, transform.position);
		visibleLine.SetPosition(1, destPos);

		//set the width as per Controller Instructions
		visibleLine.startWidth = ConnectorWidth;
		visibleLine.endWidth = ConnectorWidth;

		//The Material set to be used by the line renderer
		visibleLine.material = ConnectorMaterial;

		//remove any Shadow related data from this Line Renderer
		visibleLine.receiveShadows = false;
		visibleLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

		//ensure the Line renderer falls behind the nodes
		visibleLine.sortingLayerID = myRenderer.sortingLayerID;
		visibleLine.sortingOrder = myRenderer.sortingOrder - 2;

		//set the line renderer to 'IgnoreRaycast' Layer. So we do not try to edit it in Play Mode
		visibleLine.gameObject.layer = 2;
		//add the new line renderer to the Collection
		myLocalConnections.Add(visibleLine);
	}


	/// <summary>
	/// trying to run a late update in Editor to check for change in position of Nodes
	/// </summary>
	public void UpdatedNodePositions()
	{
		//Change all Start Positions
		RepositionStartingConnections();

		for (int i = 0; i < myDestinations.Count; i++) 
		{
			RepositionEndConnections(myDestinations[i].name, myDestinations[i].transform.position);
		}
	}

	void RepositionStartingConnections()
	{
		for (int index = 0; index < myLocalConnections.Count; index++)
		{
			myLocalConnections[index].SetPosition(0, transform.position);
		}
	}

	void RepositionEndConnections(string objName, Vector3 destPos)
	{
		for (int index = 0; index < myLocalConnections.Count; index++)
		{
			if(myLocalConnections[index].name == objName)
				myLocalConnections[index].SetPosition(0, transform.position);
		}
	}


	public void PopulateSpeedForDest()
	{
		myDestSpeed.Clear();
		foreach (Nodes item in myDestinations)
		{
			myDestSpeed.Add(3);
		}
	}

}
