using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreathTimer : MonoBehaviour {

	public float currentTime;
	public float seconds;
	public GameObject ice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider col) {
		if (col.CompareTag ("enemy")) {
			Vector3 enemyPosition = col.attachedRigidbody.gameObject.transform.position;
			ice = (GameObject)Instantiate(Resources.Load("IceBreath"), enemyPosition, Quaternion.identity);
			currentTime = Time.time;
		}
	}

	public void OnTriggerExit(Collider col) {
		if (col.CompareTag ("enemy")) {
			if (Time.time > currentTime + seconds) {
				Destroy (ice);
			}
		}
	}
}
