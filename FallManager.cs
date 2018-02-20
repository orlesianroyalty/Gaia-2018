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
	private float randomTime, time, randomX, playerZ, playerX;  
	private Vector3 original, viewPoint, teleportHere; 
	 

	// Update is called once per frame
	void Start() { 
		StartCoroutine(Fall ()); 

	} 		

	public IEnumerator Fall() { 
		while (true) { 
			//starting point of player
			playerZ = player.transform.position.z; 
			playerX = player.transform.position.x; 
			original = new Vector3 (playerX, 20, playerZ);

			viewPoint = cam.WorldToViewportPoint (original);
			Random.seed = System.DateTime.Now.Millisecond; //required, else same value keeps getting picked
			randomX =  Random.Range (-0.5f, 1.5f);
			viewPoint = new Vector3 (randomX, viewPoint.y, viewPoint.z); 

			teleportHere = cam.ViewportToWorldPoint (viewPoint); 
			teleportHere = new Vector3 (teleportHere.x, 40, teleportHere.z);

			Instantiate (fallingThing, teleportHere, Quaternion.identity);
			time = createRandomTime (); 
			print ("next one will fall in" + time); 
			yield return new WaitForSeconds (time); 
		} 
	} 

	//creates the waiting time for the falling object
	public float createRandomTime() { 

		Random.seed = System.DateTime.Now.Millisecond; //required, else same value keeps getting picked
		randomTime =  Random.Range (0f, 5.1f);
		randomTime = randomTime + 1.25f;
		return randomTime; 
	} 

}