using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShardGrassScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerStats> ().getGrassShard ();
			Destroy (gameObject);
		}
	}
}
