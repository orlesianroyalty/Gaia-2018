using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRazorLeaf : MonoBehaviour {

	private Quaternion rotation; 
	public GameObject razorLeaf; 
	public GameObject player; 


	private float timePassed = 6; 
	//seconds waited for cooldown timer
	private int coolDown = 2; 
	private bool cool = false; 


	// every frame
	void Update () { 
		//cooldown timer;
		if (cool) { 
			timePassed += Time.deltaTime; 
		} 

		if (timePassed > coolDown) {
			cool = false; 
			attack();
		} 					
	}

	//defines the attack method
	public void attack() { 
		if (Input.GetKeyDown (KeyCode.R)) {  
			rotation = new Quaternion (0.0f, 0.0f, 45f, 0.0f); 
			Vector3 here = createLaunch (); 
			Instantiate (razorLeaf, here, rotation);

			//reset timer 
			cool = true; 
			timePassed = 0; 	
		} 
	} 


	// creates launch point for razor leaf
	Vector3 createLaunch() {
		float playerX; 
		float playerY = player.transform.position.y + 0.25f; 
		Vector3 launch;


		if (!facingLeft ()) { 
			playerX = player.transform.position.x + 1.5f; 
			launch = new Vector3 (playerX, playerY,
				player.transform.position.z);	
			return launch; 
		} 
			playerX = player.transform.position.x - 1.5f; 
			launch = new Vector3 (playerX, playerY,   
				player.transform.position.z);	
			return launch; 

	}


	// returns true if facing left (uses scale b/c thats how the
	//character has been set up instead of by using rotate)
	bool facingLeft() { 
		if (player.transform.localScale.x > 0) { 
			return false; 
		} 
		return true; 
	}		
			
} 


