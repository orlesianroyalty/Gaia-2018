using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

	public int damagePerHit;
	public float hitForce;
	public string damageType = "NORMAL";

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerStats> ().damage (damagePerHit, damageType);
			Vector3 forceVector = col.gameObject.transform.position - transform.position;
			col.gameObject.GetComponent<Rigidbody> ().AddForce (forceVector * hitForce);
			Debug.Log ("Here is what the force vector looks like: " + forceVector);
		}
	}
}
