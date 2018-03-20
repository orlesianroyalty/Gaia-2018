using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Goes on player as a script component
 * Requirements: 
 * Assign the fireball GameObject
 * Put 'ProjectileFireball' script on fireball GameObject */

public class AbilityFireball : MonoBehaviour {

	public KeyCode fireKey = KeyCode.T;  //set trigger key for fireball 
	public GameObject fireball; //assign fireball object, make sure you add 'ProjectileFireball' as a script component to it

	private Camera cam; 
	private Vector3 view, position;  
	private GameObject editedFireball; 

	//for cooldown timer
	private bool cooldownNeeded = false; 
	private int cooldownTime = 5; 
	private float timePassed = 15; 


	//every frame 
	void Update () {
		if (cooldownNeeded) { 
			timePassed += Time.deltaTime; 
		} 

		if (timePassed > cooldownTime) {
			cooldownNeeded = false; 
			makeFireball();
		}
	}


	/**Launches a fireball depending on how long the user holds down 
	 * the trigger key. 'FireballProjectile' script must be a component of 
	 * 'fireball' object for this to work.  */
	void makeFireball() { 
		if (Input.GetKeyDown (KeyCode.T)) {  
			Vector3 here = createLaunch (); 
			editedFireball = Instantiate (fireball, here, Quaternion.identity);
			editedFireball.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			//for cooldown timer
			cooldownNeeded = true; 
			timePassed = 0; 
		} 

	} 

	// creates launch point for fireball
	public Vector3 createLaunch() {
		float playerX; 
		float playerY = gameObject.transform.position.y + 0.25f; 
		Vector3 launch;

		if (!facingLeft ()) { 
			playerX = gameObject.transform.position.x + 1.5f; 
			launch = new Vector3 (playerX, playerY,
				gameObject.transform.position.z);	
			return launch; 
		} 
		playerX = gameObject.transform.position.x - 1.5f; 
		launch = new Vector3 (playerX, playerY,   
			gameObject.transform.position.z);	
		return launch; 

	}


	/** returns:
	 * true if player is facing left 
	 * false if player is facing right 
	 * */
	bool facingLeft() { 
		if (gameObject.transform.localScale.x > 0) { 
			return false; 
		} 
		return true; 
	}

		
}
