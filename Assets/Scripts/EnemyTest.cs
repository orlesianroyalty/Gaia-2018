using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTest : MonoBehaviour {

	public int health;
	public bool isBlinded;
	public float damageDealt;
	public float damageTaken;
	public string weakness;
	public string strength;
	public float speed;

	private bool nearPlayer;
	private Vector3 randomx;
	private string scenename;
	private float enemyPosition;
	private float enemyBoundary1;
	private float enemyBoundary2;

	//MY STUFF
	public float rand;

	// Use this for initialization
	void Start () {
		Random.InitState (System.DateTime.Now.Millisecond);
		isBlinded = false;
		damageDealt = 1.2f;
		damageTaken = 0f;
		scenename = SceneManager.GetActiveScene().name;
		nearPlayer = false;
		enemyPosition = transform.position.x;
		enemyBoundary1 = enemyPosition - 2f;
		enemyBoundary2 = enemyPosition + 2f;
		//		if (!nearPlayer) {
		//			generateRandomMovement ();
		//		}
		//		if (nearPlayer) {
		//			moveTowardsPlayer ();
		//		}
	}

	// Update is called once per frame
	void Update () {
		Random.seed = System.DateTime.Now.Millisecond;
		rand = Random.Range (0f, 1f);
		Debug.Log (rand);
		//		if (nearPlayer) {
		//			moveTowardsPlayer ();
		//		}
		//		if (!nearPlayer) {
		//			generateRandomMovement ();
		//		}

		moveLeft ();
		//moveRight ();
	}

	void setStrengthAndWeakness() {
		if (scenename == "desert") {
			weakness = "a";
			strength = "a";
		}
		if (scenename == "arctic") {
			weakness = "d";
			strength = "d";
		}
		if (scenename == "forest") {
			weakness = "n";
			strength = "n";
		}
	}


	void onCollisionEnter(Collider col) {
		//get player sprite name
		//if player sprite name == desert   deal 1.1f damage
		//if player sprite name == arctic deal 1.7f damage
		//if player sprite name == forest deal 1.3 damage
	}

	void generateRandomMovement() {
		//float rand = Random.Range (0, 30);
		//Debug.Log ("rand called" + rand);
		if (rand <= 9) {
			stayInPlace ();
		}
		if (rand > 9 && rand <= 20) {
			moveLeft ();
		}
		if (rand > 20 && rand <= 30) {
			moveRight ();
		}
	}

	void stayInPlace() {
		//do idle animation
	}

	void moveLeft() {
		if (enemyPosition >= enemyBoundary1) {
			Vector3 vec = new Vector3 (0.5f, 0, 0);
			transform.Translate (vec * speed * Time.deltaTime);
			enemyPosition = transform.position.x;
		} 
	}

	void moveRight() {
		if (enemyPosition <= enemyBoundary2) {
			Vector3 vec = new Vector3 (-0.5f, 0, 0);
			transform.Translate (vec * speed * Time.deltaTime);
			enemyPosition = transform.position.x;
		}
	}

	void moveTowardsPlayer() {
	}

}
