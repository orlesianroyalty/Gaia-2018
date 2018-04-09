using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour {

	/// <summary>
	/// The max distance that this object can travel from where it was initialized.
	/// </summary>
	public float maxDistanceFromOrigin;

	public float speed;
	private float velocity;

	private float[] bounds = new float[2];

	void Start() {
		velocity = speed;
		bounds [0] = transform.position.x - maxDistanceFromOrigin;
		bounds [1] = transform.position.x + maxDistanceFromOrigin;
		Debug.Log (bounds [0] + " " + bounds [1]);
	}

	void Update() {
		transform.Translate(new Vector3(velocity * Time.deltaTime, 0f, 0f));
		if (transform.position.x > bounds [1] || transform.position.x < bounds [0]) {
			transform.RotateAround (transform.position, Vector3.up, 180);
		}
	}
}
