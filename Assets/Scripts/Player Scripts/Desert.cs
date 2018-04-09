using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desert : MonoBehaviour {

	public int basedmg;
	public GameObject fireball1;
	public GameObject sand;
	public GameObject player;
	public Vector3 playerPosition;
	public float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
	}

	public void fireballAttack()  {
		//instantiate fireball animation
		fireball1 = (GameObject)Instantiate (Resources.Load("Fireball"), playerPosition, Quaternion.identity);
		//make fireball move in direction the player fires it
		int direction = player.GetComponent<PlayerMovement>().getFacingDirection();
		if (direction == 1) {
		} else if (direction == -1) {
			fireball1.transform.localScale = new Vector3 (fireball1.transform.localScale.x * -1, fireball1.transform.localScale.y, fireball1.transform.localScale.z);
			fireball1.GetComponent<ProjectileMove> ().speed *= -1f;
			fireball1.GetComponent<ProjectileMove> ().damage = basedmg;
		}
		//on collision enter start impact animation
	}

	public void poweredUpFireball(){
		int doubledmg = basedmg * 2;
		fireball1 = (GameObject)Instantiate (Resources.Load("Fireball"), playerPosition, Quaternion.identity);
		//make fireball move in direction the player fires it
		int direction = player.GetComponent<PlayerMovement>().getFacingDirection();
		if (direction == 1) {
		} else if (direction == -1) {
			fireball1.transform.localScale = new Vector3 (fireball1.transform.localScale.x * -1, fireball1.transform.localScale.y, fireball1.transform.localScale.z);
			fireball1.GetComponent<ProjectileMove> ().speed *= -1f;
			fireball1.GetComponent<ProjectileMove> ().damage = doubledmg;
		}
		//on collision enter start impact animation
	}

	public void sandstorm() {
		sand = (GameObject)Instantiate(Resources.Load("sandstorm"), playerPosition, Quaternion.identity);
	}

	public float getBaseDamage() {
		return basedmg;
	}


}
