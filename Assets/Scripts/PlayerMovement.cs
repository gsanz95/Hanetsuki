using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float movementSpeed;
	[SerializeField] float jumpSpeed;
	[SerializeField] float lerpSpeed;
	[SerializeField] float objectGravity;
	public Rigidbody playerRigidbody;
	public float lookSensitivityX;
	public float lookSensitivityY;
	private bool isOnGround;
	private bool isRising;
	private float risingTime = .3f;
	private float axisMovementX;
	private float axisMovementY;
	private float axisRotationX;
	private float axisRotationY;
	private bool isTryingToJump;
	private Vector3 desiredPosition;
	private float deltaMovementX;
	private float deltaMovementY;
	private float deltaRotationX;
	private float deltaRotationY;
	
	// Update is called once per frame
	void Update (){
		axisMovementX = Input.GetAxisRaw("LeftStickHorizontal");
		axisRotationX = Input.GetAxisRaw("RightStickHorizontal");
		axisRotationY = Input.GetAxisRaw("RightStickVertical");
		//axisMovementY = Input.GetAxisRaw("JetPack");

		setIsTryingToJump(Input.GetButton("Jump"));
		Debug.Log(isTryingToJump);
		Debug.Log(isRising);
		Debug.Log(risingTime);

	}

	void setIsTryingToJump(bool isJumping){
		isTryingToJump = isJumping;
	}

	void FixedUpdate(){
		AxisUpdateX();
		AxisUpdateY();

		SetDesiredPosition();
		UpdateRigidBodyPosition();
		UpdateRigidbodyRotation();
	}

	void AxisUpdateX(){
		// Translation
		deltaMovementX = axisMovementX * movementSpeed;
		//Adjust for time change
		deltaMovementX *= Time.fixedDeltaTime;

		// Rotation
		deltaRotationX = axisRotationX * lookSensitivityX;
	}

	void AxisUpdateY(){

		// Translation
		if(isTryingToJump && isOnGround){
			tryJump();
		}else if(isRising){
			continueRising();
		}else{
			deltaMovementY = 0f;
			deltaMovementY += 0.5f *objectGravity * Time.fixedDeltaTime;
		}

		// Adjust for time change
		deltaMovementY *= Time.fixedDeltaTime;

		// Rotation
		deltaRotationY = axisRotationY * lookSensitivityY;
	}

	void tryJump(){
		setIsRising(true);
		deltaMovementY = jumpSpeed;
	}

	void setIsRising(bool risingValue){
			isRising = risingValue;
	}

	void continueRising(){
		if(risingTime >= 0f){
			deltaMovementY = jumpSpeed;
			risingTime -= Time.fixedDeltaTime;
		}else{
			setIsRising(false);
		}
	}

	public void setRisingTime(float risingTimeValue){
		risingTime = risingTimeValue;
	}

	public void setIsOnGround(bool onGroundValue){
		isOnGround = onGroundValue;
	}

	void SetDesiredPosition(){
		desiredPosition = playerRigidbody.position;
		desiredPosition += new Vector3(Mathf.MoveTowards(0,deltaMovementX,lerpSpeed), Mathf.MoveTowards(0,deltaMovementY,lerpSpeed), 0);
	}

	void UpdateRigidBodyPosition(){
		playerRigidbody.position = desiredPosition;
	}

	void UpdateRigidbodyRotation(){
		playerRigidbody.MoveRotation(Quaternion.Euler(deltaRotationX,deltaRotationY,0f));
	}
}
