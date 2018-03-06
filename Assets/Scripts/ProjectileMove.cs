using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

	public float speed = 4f;
	public int damage = 0;
	private float lifetimeStart;
	private float lifetimeEnd;
	private float lifetimeInSeconds = 2f;

	void Start () {
		lifetimeStart = Time.time;
		lifetimeEnd = lifetimeStart + lifetimeInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (speed * Time.deltaTime, 0f, 0f));
		if (Time.time > lifetimeEnd) {
			Destroy (gameObject);
		}
	}
}
