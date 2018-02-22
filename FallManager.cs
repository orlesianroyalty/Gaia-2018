using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Instantiates falling objects within a small time 
 * of each other. 
 * */
public class FallManager : MonoBehaviour {

	public GameObject fallingThing; 
	public GameObject player; 
	public Camera cam; 

	private float randomTime, time, randomX, playerY; 
	private Vector3 original, viewPoint, teleportHere; 
		
	void Start() { 
		StartCoroutine(Fall ()); 

	} 		

	/* central method, makes objects fall at somewhat random coordinates (near player)
	 * If you want objects to fall from a different height, edit the number being added to playerY = ..
	 * If you want objects to fall at greater gaps of time or smaller, edit createRandomTime (...); 	
	 * and/or randomTime += @ line 52
	 * If you want objects to fall at a greater or smaller possible range from player, edit randomX = .. under the 
	 * fallingCoordinate() method. 
	 * */
	public IEnumerator Fall() {
		while (true) { 
			if (blocked ()) { 
				playerY = player.transform.position.y + 4.5f;
			} else { 				
				playerY = player.transform.position.y + 9; 
			} 	

			teleportHere = fallingCoordinate (playerY);
			Instantiate (fallingThing, teleportHere, Quaternion.identity);
			time = createRandomTime (0f, 2f); 
			//print ("next one will fall in" + time); */
			yield return new WaitForSeconds (time); 
			}

	}  


	//creates the waiting time for the falling object
	public float createRandomTime(float bound1, float bound2) { 

		Random.seed = System.DateTime.Now.Millisecond; //required, else same value keeps getting picked
		randomTime =  Random.Range (bound1, bound2);
		randomTime += 0.3f;
		return randomTime; 
	} 


	//checks if an object, ex: platform, exists aboev player that would
	//block a falling object
	public bool blocked() { 
		if (Physics.Raycast (player.transform.position, Vector3.up, 9)) { 
			//print ("Platform exists above player");
			return true; 
		} else { 
			return false; 
		} 
	} 


	/*Returns random coordinate where falling object will be instantiated
	 * 
	 * */
	public Vector3 fallingCoordinate(float yValue) { 
		Vector3 coordinate = new Vector3 (); 

		original = new Vector3 (player.transform.position.x, yValue, 
			player.transform.position.z);
		viewPoint = cam.WorldToViewportPoint (original);

		Random.seed = System.DateTime.Now.Millisecond; //required, else same value keeps getting picked
		randomX =  Random.Range (0.1f, 0.9f);
		viewPoint = new Vector3 (randomX, viewPoint.y, viewPoint.z); 

		coordinate = cam.ViewportToWorldPoint (viewPoint); 
		print ("Final1: " + coordinate); 
		coordinate = new Vector3 (coordinate.x, coordinate.y, coordinate.z);

		return coordinate; 
	} 
		


}