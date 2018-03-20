using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Goes on fireball object, you guys will have to specify how much damage you want this to cause, 
 * do this under method 'damageEnemy()' in this script */
public class ProjectileFireball : MonoBehaviour {

	private Camera cam; 
	private float facing; 
	private bool fired = false; 
	private GameObject player; 
	private Vector3 view, position; 
	private float speed = 20f; 
	private float size, elapsedTime; 


	void Start() {
		facing = 0; 
		size = 0.5f; 
		elapsedTime = 0.0f; 
		cam = Camera.main; 
		player = GameObject.Find ("Player"); 
	} 


	//every frame
	void Update() {
		shootFireball (); 
		if (!isInView ()) { 
			damageEnemy (); 
			Destroy (gameObject); 
		} 
	} 

	//charges up the fireball 
	void shootFireball() { 
		if (!fired) { 
			if (Input.GetKey (KeyCode.T) && elapsedTime < 2.0f) { 
				elapsedTime += Time.deltaTime; 
				size += (elapsedTime / 100.0f); 
				gameObject.transform.position = createLaunch (); 
				gameObject.transform.localScale = new Vector3 (size, size, size); 
			}
			if (Input.GetKeyUp (KeyCode.T) || elapsedTime > 2.0f) { 
				facing = player.transform.localScale.x; 
				fired = true;  
			}
		} 
		if ( facing > 0) { 
			transform.Translate (Vector3.right * speed * Time.deltaTime, relativeTo: Space.World);
		} 
		if ( facing < 0) { 
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


		// creates launch point for fireball
		public Vector3 createLaunch() {
			float playerX; 
			float playerY = player.transform.position.y + 0.25f; 
			Vector3 launch;

			if (!facingLeft ()) { 
				playerX = player.transform.position.x + 1.5f; 
				launch = new Vector3 (playerX, playerY,
					player.transform.position.z);	
				return launch; 
			} 
			playerX = player.transform.position.x - 1.5f; 
			launch = new Vector3 (playerX, playerY,   
				player.transform.position.z);	
			return launch; 

		}


		/** returns
	  * true if facing left 
	  * false if facing right 
	  * */
		bool facingLeft() { 
			if (player.transform.localScale.x > 0) { 
				return false; 
			} 
			return true; 
		}


		/*damages enemy based on size of fireball 
		 * */
		void damageEnemy() { 
		    if (gameObject.transform.localScale.x >= 1.5f) { 
				//script to damage enemy maximum amount
				return; 
			} 
			if (gameObject.transform.localScale.x >= 0.75) { 
			//script to damage enemy medium amount
			return; 
			}
			//script to damage enemy minimum amount
		} 

	}  
