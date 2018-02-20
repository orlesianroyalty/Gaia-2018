using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

	private Transform player;

	public float smoothFactor = 12f;
	public Vector3 offset;

	void Start() {
		player = GameObject.Find ("Player").transform;
	}

	void LateUpdate() {

		Vector3 targetPos = player.position + offset;
		Vector3 smoothedPos = Vector3.Lerp (transform.position, targetPos, smoothFactor * Time.deltaTime);
		transform.position = smoothedPos;

		transform.LookAt (player);
	}
}