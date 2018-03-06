using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStormScript : MonoBehaviour {

	private float lifetimeStart;
	private float lifetimeEnd;
	private float lifetimeInSeconds = 5f;

	// Use this for initialization
	void Start () {
		lifetimeStart = Time.time;
		lifetimeEnd = lifetimeStart + lifetimeInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lifetimeEnd) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<EnemyStats> ().stun (4f);
			Debug.Log ("ENEMY");
		}
	}
}
