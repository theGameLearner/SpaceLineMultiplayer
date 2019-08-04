using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeColorLerper : MonoBehaviour
{
	[Header("RangeSize")]
	public float turretRange = 0.75f;
	[Header("Colors")]
	public Color startColor;
	public Color endColor;

	[Header("Color LifeTime")]
	[Tooltip("The time between turret firings")]
	public float fireTime;

	[Header("External references")]
	public SpriteRenderer turrentRangeSprite;


	//Private Variables
	private float turretTime;

	void Start()
	{
		turrentRangeSprite.transform.localScale = Vector3.one * turretRange;
	}

    // Update is called once per frame
    void Update()
    {
		turretTime -= Time.deltaTime;
		if (turretTime <= 0)
		{
			turretTime = fireTime;
			turrentRangeSprite.color = startColor;
			return;
		}

		turrentRangeSprite.color = Color.Lerp(startColor, endColor, (fireTime - turretTime) / fireTime);
	}
}
