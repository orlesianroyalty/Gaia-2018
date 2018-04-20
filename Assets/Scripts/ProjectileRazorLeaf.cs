using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRazorLeaf : MonoBehaviour {
	
	private Vector3 view, shurikenPosition; 
	private float speed = 20f; 
	private GameObject player; 
	private EnemyStats eStats; 
	private Camera cam; 
	private float x; 


	void Awake() { 
		player = GameObject.Find ("Player"); 
		cam = Camera.main; 
		x = player.transform.localScale.x; 
	} 

	/*every frame*/
	void Update() {
		moveLeaf(); 
		if (!isInView ()) { 
			Destroy (gameObject); 
		} 
	} 

	/*Describes motion for the object*/
	void moveLeaf() { 
		//throw right
		if (x > 0) { 
				transform.Translate (Vector3.right * speed * Time.deltaTime, relativeTo: Space.World);
				transform.Rotate ( -1 * Vector3.forward, 2000f * Time.deltaTime);  
		}
		//throw left
		else { 	transform.Translate (Vector3.left * speed * Time.deltaTime, relativeTo: Space.World);
			transform.Rotate (Vector3.forward, 2000f * Time.deltaTime);  
		}
	}


	/*Returns true if object is in view 
	 *if not, returns false
	 * */
	bool isInView() { 
		shurikenPosition = gameObject.transform.position; 
		view = cam.WorldToViewportPoint (shurikenPosition); 
		if (view.x > 1.2 || view.x < -0.2) { 
			return false;
		} 
		return true;
	} 
		

	/*Inflicts 8 damage on enemy if hit 
	 * */
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Enemy") { 
			GameObject enemy = collision.gameObject; 
			eStats = enemy.GetComponent<EnemyStats> (); 
			eStats.damage (8, "NORMAL"); 
		} 
	}
		
}

 
