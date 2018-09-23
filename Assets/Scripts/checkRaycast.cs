using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkRaycast : MonoBehaviour
{

	public float damage = 10f;
	public float range = 100f;
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("PS4_R2") || Input.GetButtonDown("LeftClick"))
		{
			Shoot();
		}
		Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 10, Color.black, Time.deltaTime);
	}

	void Shoot()
	{
		RaycastHit raySensor;
		if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out raySensor, range))
		{
			Debug.Log(raySensor.transform.name);

			actorHealth targetHealth = raySensor.transform.GetComponent<actorHealth>();

			if(targetHealth != null)
			{
				targetHealth.takeDamage(20f);
			}


		}
	}
}
