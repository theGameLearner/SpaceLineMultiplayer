using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
	public GameObject myTarget;

	private Transform targetTransform;
	float angle;
	Quaternion q;

	void Start()
    {
		targetTransform = myTarget.transform;
	}
    
    void Update()
    {
		//find the direction we want to point to
		Vector3 vectorToTarget = targetTransform.position - transform.position;
		//not sure
		angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg ;
		//rotate around right angle
		q = Quaternion.AngleAxis(angle, Vector3.forward);
		//actual rotation is changed using slerp
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);

	}
}
