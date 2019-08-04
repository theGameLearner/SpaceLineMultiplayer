using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurretAimSprites : MonoBehaviour
{
	[Header("Prefab to create Aim Points")]
	public GameObject turretAimGob;
	[Header("List of aimer and Targets")]
	public Dictionary<AimTurret, Transform> myAimList = new Dictionary<AimTurret, Transform>();
	//Private Variables
	int noOfPlayers = 2;
	GameObject newPlayerAimer;

    // Start is called before the first frame update
    void Start()
    {
		noOfPlayers = gameController.instance.NoOfPlayers;
		for(int i = 0; i< noOfPlayers; i++)
		{
			newPlayerAimer = Instantiate(turretAimGob, transform.position, Quaternion.identity);
			newPlayerAimer.name = "Turret Aim_player" + i.ToString("00");
			newPlayerAimer.SetActive(true);
			newPlayerAimer.transform.parent = transform;
			newPlayerAimer.transform.localScale = turretAimGob.transform.localScale;
			newPlayerAimer.GetComponent<AimTurret>().myTarget = gameController.instance.players[i];
			myAimList.Add(newPlayerAimer.GetComponent<AimTurret>(), gameController.instance.players[i].transform);
		}

	}
}
