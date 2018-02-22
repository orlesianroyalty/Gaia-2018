using UnityEngine;

/**Makes the object fall + spin 
 * */
public class FallingObject : MonoBehaviour {


	public float fallSpd; 
	private Collider coll; 
	private PlayerStats pStats = new PlayerStats(); 

	//fall and spin boi
	void Update() { 
		transform.Translate (Vector3.down * fallSpd * Time.deltaTime, relativeTo: Space.World);
		transform.Rotate (Vector3.forward, 200f * Time.deltaTime);  
		transform.Rotate (Vector3.left, 250f * Time.deltaTime);  
	}


	//Possibility 1: damage player + get destroyed
	//Poss 2: hit floor + get destroyed rip
	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Player") { 
			Destroy (gameObject); 
			pStats.damage (30, "NORMAL");

		}

		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Shard") { 
			Destroy (gameObject); 
		} 
	}


	 
}