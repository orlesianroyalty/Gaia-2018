using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour {

	public float basedamage;
	public Vector3 playerPosition;
	public GameObject player;
	public GameObject leaves;
	public float speed;

	// Use this for initialization
	void Start () {
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void razorLeafAttack() {
		leaves = (GameObject)Instantiate (Resources.Load ("Prefabs/Sphere"), playerPosition, Quaternion.identity);
		leaves.transform.position -= leaves.transform.up * speed * Time.deltaTime;
	}
}
