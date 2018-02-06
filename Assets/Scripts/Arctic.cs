using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arctic : MonoBehaviour {

	public int basedamage;
	public GameObject iceblock;
	public GameObject icebreath;
	public GameObject player;
	public Vector3 playerPosition;
	public float speed;
	public float seconds;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SnowStormAttack() {
		basedamage = 2;
		GameObject snowstorm = (GameObject)Instantiate (Resources.Load ("SnowStorm"), GameObject.FindGameObjectWithTag ("Player").transform.position, Quaternion.identity); 

	}

	public void iceBreathAttack() {
		basedamage = 0;
		icebreath = (GameObject)Instantiate (Resources.Load ("IceBreath"), GameObject.FindGameObjectWithTag ("Player").transform.position, Quaternion.identity); 
		int direction = player.GetComponent<PlayerMovement>().getFacingDirection();
		if (direction == 1) {
		} else if (direction == -1) {
			icebreath.transform.localScale = new Vector3 (icebreath.transform.localScale.x * -1, icebreath.transform.localScale.y, icebreath.transform.localScale.z);
			icebreath.GetComponent<ProjectileMove> ().speed *= -1f;
			icebreath.GetComponent<ProjectileMove> ().damage = basedamage;
		}

	}

}
