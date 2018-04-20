using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FIXES: EDIT drainStamina(); 
//ASSIGN SPRITES

/*Goes on fireball object, you guys will have to specify how much damage you want this to cause, 
 * do this under method 'damageEnemy()' in this script */
public class ProjectileFrost : MonoBehaviour {

	private Camera cam; 
	private GameObject player;
	private PlayerStats pStats; 
	private EnemyStats eStats; 
	private Abilities pAbs; 

	//for shootFrost and creatLaunch methods
	private float facing; 
	private KeyCode frostKey; 
	private Vector3 view, position; 
	private float speed = 10f; 
	private float size, elapsedTime; 


	void Start() {
		size = 1f; 
		elapsedTime = 0.0f; 
		cam = Camera.main; 
		player = GameObject.Find ("Player"); 
		pStats = player.GetComponent<PlayerStats> (); 
		pAbs = player.GetComponent<Abilities> ();
		frostKey = pAbs.frostKey; 
		facing = player.transform.localScale.x;
	} 


	//every frame
	void Update() {
		shootFrost (); 
		if (!isInView ()) { 
			//damageEnemy (); 
			Destroy (gameObject); 
		} 
	} 


	//charges up and shoots the fireball 
	//For debugging: most recent add: 3rd or statement in 3rd if statement
	void shootFrost() {
		if (Input.GetKey (frostKey)) {
				if (size < 3.0f) { 
					size += Time.deltaTime; 
					gameObject.transform.localScale = new Vector3 (size, size, 0.2f); 
			} 
		}

		//add an if collided w enemy statement
		if ( facing > 0) { 
			transform.Rotate ( -1 * Vector3.forward, 2000f * Time.deltaTime);  
			transform.Translate (Vector3.right * speed * Time.deltaTime, relativeTo: Space.World);
		} 

		if ( facing < 0) { 
			transform.Rotate ( -1 * Vector3.forward, 2000f * Time.deltaTime);  
			transform.Translate (Vector3.left * speed * Time.deltaTime, relativeTo: Space.World); 
		}
	} 



	/*Returns true if object is in view of main camera
	 *if not, returns false
	 * */
	bool isInView() { 
		position = gameObject.transform.position; 
		view = cam.WorldToViewportPoint (position); 
		if (view.x > 1.3 || view.x < -0.3) { 
			return false;
		} 
		return true;
	} 


	/* */
	void OnTriggerEnter(Collider collider) {

		if (collider.gameObject.tag == "Enemy") { 
			GameObject enemy = collider.gameObject; 
			eStats = enemy.GetComponent<EnemyStats> (); 
			eStats.damage (1, "NORMAL"); 
			//estats.frozen = true; 
		} 
	}

}  
