using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

	public float speed;

	private bool nearPlayer;
	private Vector3 randomx;
	private string scenename;
	private float enemyPosition;
	private float enemyBoundary1;
	private float enemyBoundary2;
	private Rigidbody rb;

	private GameObject player;

	void Start () {
		Random.InitState (System.DateTime.Now.Millisecond);
		scenename = SceneManager.GetActiveScene().name;
		nearPlayer = false;
		enemyPosition = transform.position.x;
		enemyBoundary1 = enemyPosition - 2f;
		enemyBoundary2 = enemyPosition + 2f;
		rb = GetComponent<Rigidbody> ();
		//StartCoroutine (wait ());
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//if (nearPlayer) {
		if (!GetComponent<EnemyStats> ().stunned) {
			moveTowardsPlayer ();
		}
		//}
//		if (!nearPlayer) {
//			generateRandomMovement ();
//		}

	}

	void onCollisionEnter(Collider col) {
		//get player sprite name
		//if player sprite name == desert   deal 1.1f damage
		//if player sprite name == arctic deal 1.7f damage
		//if player sprite name == forest deal 1.3 damage
	}

	void generateRandomMovement() {
		Random.seed = System.DateTime.Now.Millisecond;
		float rand = Random.Range (0, 30);
		Debug.Log ("rand called" + rand);
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
			Debug.Log ("movedleft");
			Vector3 vec = new Vector3 (1f, 0, 0);
			rb.AddForce (vec * speed * Time.deltaTime);
			//transform.Translate (vec * speed * Time.deltaTime);
			enemyPosition = transform.position.x;
		} 
	}

	void moveRight() {
		if (enemyPosition <= enemyBoundary2) {
			Debug.Log ("movedright");
			Vector3 vec = new Vector3 (1f, 0, 0);
			rb.AddForce (vec * speed * Time.deltaTime);
			//transform.Translate (vec * speed * Time.deltaTime);
			enemyPosition = transform.position.x;
		}
	}

	void moveTowardsPlayer() {
		if (player != null) {
			Vector3 targetVector = player.transform.position;
			transform.position = Vector3.MoveTowards (transform.position, targetVector, .02f);
			if (player.transform.position.x > transform.position.x) {
				transform.localScale = new Vector3 (-.3f, transform.localScale.y, transform.localScale.z);
			} else {
				transform.localScale = new Vector3 (.3f, transform.localScale.y, transform.localScale.z);
			}
		}
	}

	IEnumerator wait() {
		while (!nearPlayer) {
			generateRandomMovement ();
			yield return new WaitForSeconds (5f);
		}
	}


}
