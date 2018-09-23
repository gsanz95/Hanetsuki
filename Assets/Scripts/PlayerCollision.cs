using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
	public PlayerMovement playerMovement;
	void OnCollisionEnter(Collision collisionInfo){
		if(collisionInfo.collider.tag.Equals("Ground")){
			playerMovement.setIsOnGround(true);
			playerMovement.setRisingTime(.3f);
		}
	}

	void OnCollisionExit(Collision collisionInfo){
		if(collisionInfo.collider.tag.Equals("Ground")){
			playerMovement.setIsOnGround(false);
		}
	}
}
