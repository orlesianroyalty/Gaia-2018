using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Destroys falling object when it hits le floor
 * */
public class DestroyFalling : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		print (collision.gameObject.tag); 
		if (collision.gameObject.tag == "Floor") { 
			Destroy (gameObject); 
			print ("destroyed"); 
		} 
	}
} 

