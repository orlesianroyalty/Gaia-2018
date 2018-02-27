using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public KeyCode jumpButton = KeyCode.Space;
	public KeyCode dashButton = KeyCode.LeftShift;

	public float speed = 5.5f;

	//When the jump button is pressed, the player will launch up (y axis) with this minimum force (or x for dash)
	public float initialJumpForce = 7.2f;
	public float initialDashForce = 7f;

	//factor that the acceleration is multipled by to achieve variable jumps/dashes (100-200 is preferable for jumps)
	public float jumpAccelerationFactor = 300f;
	public float dashAccelerationFactor = 370f;

	public int dashStaminaCost = 20;
	public int arcJumpStaminaCost = 25;

	public float arcJumpForceX = 140f;
	public float arcJumpForceY = 370f;

	//cooldown values for the dash
	public float dashMaxDurationTimeInSeconds = .4f;
	private float dashStartTimeStamp;
	private float dashEndTimeStamp;

	//controls
	private float horizontalInputFactor;
	private float verticalInputFactor;

	private Vector3 currentVelocityVector;

	private Rigidbody rb;
	private Animator ani;
	private PlayerStats stats;

	public bool isOnGround;
	public bool isDashing = false;
	private int facingDirection = 1;
	public bool movementLocked;

	public float fireballHoldDurationInSeconds = 3f;

	float fireballTimeStampStart;
	float fireballTimeStampEnd;

	bool fireballActive = false;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		ani = GetComponent<Animator> ();
		stats = GetComponent<PlayerStats> ();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor") {
			isOnGround = true;
			ani.SetBool ("isOnGround", true);
			movementLocked = false;
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Floor") {
			isOnGround = false;
			ani.SetBool ("isOnGround", false);
		}
	}

	void Update () {

		ElementalTypeChangeCheck ();

		BasicAttackCheck ();

		JumpCheck ();
		DashCheck ();

		if (!movementLocked) {
			horizontalInputFactor = Input.GetAxis ("Horizontal"); //left/right (x)
			verticalInputFactor = Input.GetAxis ("Vertical"); //depth (z)
		} else {
			horizontalInputFactor = 0;
			verticalInputFactor = 0;
		}

		Vector3 movement = new Vector3 (horizontalInputFactor, 0.0f, verticalInputFactor);

		transform.Translate (movement * speed * Time.deltaTime);

		currentVelocityVector = rb.velocity;

		if (horizontalInputFactor > 0) {
			facingDirection = 1;
			correctFacingDirection();
		} else if (horizontalInputFactor < 0) {
			facingDirection = -1;
			correctFacingDirection();
		}

		if ((horizontalInputFactor != 0 || verticalInputFactor != 0) && isOnGround && !isDashing) {
			ani.SetBool ("isMoving", true);
		} else {
			ani.SetBool ("isMoving", false);
		}
	}
		
	/**
	 * Handles Variable jumping mechanic. Also handles Arc jumping.
	 **/
	void JumpCheck() {
		
		if (Input.GetKeyDown (jumpButton) && isOnGround && !isDashing) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				if (stats.useStamina (arcJumpStaminaCost)) {
					rb.AddForce (new Vector3 (facingDirection * arcJumpForceX, arcJumpForceY, 0.0f));
					if (stats.getElementalType() == "ICE") {
						GetComponent<Arctic> ().SnowStormAttack();
					}
					movementLocked = true; 
				}
			} else {
				rb.AddForce (Vector3.up * initialJumpForce, ForceMode.VelocityChange);
			}
		}

		if(Input.GetKey(jumpButton) && !isOnGround && !movementLocked ) {
			if (currentVelocityVector.y > 0f) {
				rb.AddForce (Vector3.up * jumpAccelerationFactor * Time.deltaTime, ForceMode.Acceleration);
			}
		} 
	}

	/**
	 * Handles Variable jumping mechanic.
	 **/
	void DashCheck() {
		
		if (Input.GetKeyDown (dashButton) && isOnGround && !isDashing && horizontalInputFactor != 0) {
			if (stats.useStamina (dashStaminaCost)) {
				isDashing = true;

				dashStartTimeStamp = Time.time;
				dashEndTimeStamp = dashStartTimeStamp + dashMaxDurationTimeInSeconds;

				//make horizontal input factor 1 or -1 as long as there is input
				int dashDirection = 0;
				if (horizontalInputFactor > 0) {
					dashDirection = 1;
				} else if (horizontalInputFactor < 0) {
					dashDirection = -1;
				}

				rb.AddForce (Vector3.right * initialDashForce * dashDirection, ForceMode.VelocityChange);
			}
		}

		if (Input.GetKey (dashButton) && isDashing) {
			rb.AddForce (Vector3.right * dashAccelerationFactor * horizontalInputFactor * Time.deltaTime, ForceMode.Acceleration);
		}

		if (Time.time > dashEndTimeStamp) {
			isDashing = false;
		}
	}

	void ElementalTypeChangeCheck() {
		
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			stats.changeElementalTypeToGrass ();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			stats.changeElementalTypeToFire ();
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			stats.changeElementalTypeToIce ();
		}
	}

	void BasicAttackCheck() {
		
		if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) {
			if (stats.getElementalType() == "FIRE") {
				fireballTimeStampStart = Time.time;
				fireballTimeStampEnd = fireballTimeStampStart + fireballHoldDurationInSeconds;
				fireballActive = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) && !isOnGround) {
			if (stats.getElementalType() == "GRASS") {
				//RAZOR LEAF
			}
		}

		if (fireballActive) {
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				Debug.Log ("Key Released:" + (Time.time - fireballTimeStampEnd));
				if ((Time.time - fireballTimeStampEnd) > fireballHoldDurationInSeconds) {
					GetComponent<Desert> ().poweredUpFireball();
					Debug.Log ("big fireball called");
				} else {
					GetComponent<Desert> ().fireballAttack();
					Debug.Log ("mini fireball called");
				}
				fireballActive = false;
			}
		}
	}

	/** returns 1 if facing right, returns -1 if left
	 * 
	 **/
	public int getFacingDirection () {
		return facingDirection;
	}

	void correctFacingDirection() {
		transform.localScale = new Vector3 (facingDirection, transform.localScale.y, transform.localScale.z);
	}
}
