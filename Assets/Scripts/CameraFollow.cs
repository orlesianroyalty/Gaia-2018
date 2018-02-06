using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform player;

	void Start () {
		player = GameObject.Find ("Player").transform;
	}

	void Update () {
		if (player != null) 
			transform.position = new Vector3 (player.position.x, transform.position.y, -10);
	}
}