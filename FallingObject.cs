using UnityEngine;

/**Makes the object fall + spin 
 * */
public class FallingObject : MonoBehaviour {


	public float fallSpd = 7.0f;
	public float spinSpd = 200f; 
	private Collider coll; 
	//public float spin2Spd = 150f; 


	// Update is called once per frame
	void Awake() { 
	} 

	void Update() { 

		transform.Translate (Vector3.down * fallSpd * Time.deltaTime, relativeTo: Space.World);
		transform.Rotate (Vector3.forward, spinSpd * Time.deltaTime);  
		transform.Rotate (Vector3.right, spinSpd * Time.deltaTime);  
	}


	 
}