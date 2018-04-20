using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSandstorm : MonoBehaviour {

	private Camera cam; 
	private Vector3 view, position; 
	private float timer = 0; 
	private float pX, y, size, radiusMultiplier, speedMultiplier, delay, elapsedTime;
	private GameObject player; 
	private EnemyStats eStats; 
	private Abilities pAbs; 


	// Use this for initialization
	void Start () {
		size = 0.1f; 
		delay = 0.0f; 
		radiusMultiplier = 1f; 
		speedMultiplier = 6f; 
		cam = Camera.main; 
		player = GameObject.Find ("Player"); 
		pAbs = player.GetComponent<Abilities> ();
		//pX = pAbs.sandstormOrigin; //if you want sandstorm to stay in one place while player moves
		pX = player.transform.position.x; 
		y = player.transform.position.y + -.5f; 
	
		
	}
	
	// Update is called once per frame
	void Update () { 
		moveSand (); /*
		if (timer > 14.5f) {  
			Destroy (gameObject); 
		} */
		if (timer > 7.0f) { 
			Destroy (gameObject); 
		} 
	} 

	// sand moves in a circular motion
	void moveSand() { 
		timer += Time.deltaTime;
		float x = (radiusMultiplier * Mathf.Cos (speedMultiplier * timer)) + pX;
		float z = Mathf.Sin (speedMultiplier * timer) * radiusMultiplier;
		transform.position = new Vector3 (x, transform.position.y, z); 
			//y, z);
		gameObject.transform.localScale = new Vector3 (size, size, size); 

		radiusMultiplier += 0.021f; 
		if (speedMultiplier < 5f) { 
			speedMultiplier += 0.03f; 
		} 
		if (y < 5.5f) { 
			y += 0.018f; 
		} else {
			y -= .03f;
		} 
		delay++; 
		if (size < 0.75f && delay % 9 == 0) { 
			size += 0.025f; 
		} 

	} 

	/*Returns true if object is in view of main camera
	 *if not, returns false
	 * */
	/* */
	void OnTriggerEnter(Collider collider) {

		if (collider.gameObject.tag == "Enemy") { 
			GameObject enemy = collider.gameObject; 
			eStats = enemy.GetComponent<EnemyStats> (); 
			//estats.sanded = true; 
		} 
	}
		
}
