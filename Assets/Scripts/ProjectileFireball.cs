using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FIXES: EDIT drainStamina(); 
//ASSIGN SPRITES

/*Goes on fireball object, you guys will have to specify how much damage you want this to cause, 
 * do this under method 'damageEnemy()' in this script */
public class ProjectileFireball : MonoBehaviour {

	private Camera cam; 
	private GameObject player;
	private PlayerStats pStats; 
	private EnemyStats eStats; 

	//for shootFireball and creatLaunch methods
	private float facing; 
	private bool fired = false; 
	private Vector3 view, position; 
	private float speed = 12f; 
	private float size, elapsedTime; 

	//sprite variables
	private int delay = 0; 
	private int imgNum = 0; 
	private Sprite[] fireSprites; 
	private bool spriteInitializedLeft = false; 
	private bool spriteFacingLeft; 
	private SpriteRenderer spriteRender; 


	void Start() {
		facing = 0; 
		size = 0.5f; 
		elapsedTime = 0.0f; 
		cam = Camera.main; 
		player = GameObject.Find ("Player"); 
		pStats = player.GetComponent<PlayerStats> (); 
		spriteRender = GetComponent<SpriteRenderer> (); 
		fireSprites = Resources.LoadAll<Sprite> ("fireball");  
		//print (fireSprites.Length); 
		if (player.transform.localScale.x < 0) { 
			spriteRender.flipX = true; 
			spriteInitializedLeft = true; 
		} 
	} 


	//every frame
	void Update() {
		shootFireball (); 
		if (!isInView ()) { 
			//damageEnemy (); 
			Destroy (gameObject); 
		} 
	} 


	//charges up and shoots the fireball 
	//For debugging: most recent add: 3rd or statement in 3rd if statement
	void shootFireball() { 
		if (!fired) { 
			if (Input.GetKey (KeyCode.T) && elapsedTime < 2.0f) {
					setSprite(); 
					flipSprite(); 
					elapsedTime += Time.deltaTime; 
					size += (elapsedTime / 100.0f); 
					gameObject.transform.position = createLaunch (); 
					gameObject.transform.localScale = new Vector3 (size, size, size); 

			}

			if (Input.GetKeyUp (KeyCode.T) 
					|| elapsedTime > 2.0f
						|| !pStats.useStamina((int)Time.deltaTime * 10)) { 

				facing = player.transform.localScale.x; 
				fired = true;  
			}
		} 
		//add an if collided w enemy statement
		if ( facing > 0) { 
			setSprite (); 
			transform.Translate (Vector3.right * speed * Time.deltaTime, relativeTo: Space.World);
		} 

		if ( facing < 0) { 
		setSprite(); 
			transform.Translate (Vector3.left * speed * Time.deltaTime, relativeTo: Space.World); 
		}
	} 

	/**damages enemy based on size of fireball 
	 * */
	int getDamageMultiplier() { 
		if (size >= 1.45f) { 
			return 25; 
		} 
		if (size >= 0.8) { 
			return 15; 
		}
		return 10; 
	} 
	
	/* Maintains correct direction of fireball sprite so that its facing the same direction as the player
	 * */
	void flipSprite() { 
		if (spriteInitializedLeft) { 			
			if (!facingLeft () && spriteRender.flipX) {
				spriteRender.flipX = false; 
				return;
			} 
			if (facingLeft () && !spriteRender.flipX) { 
				spriteRender.flipX = true; 
				return;
			} 
		} else { 
			if (facingLeft () && !spriteRender.flipX) {
				spriteRender.flipX = true; 
				return;
			} 
			if (!facingLeft () && spriteRender.flipX) { 
				spriteRender.flipX = false; 
				return;
			}
		} 
	}

	/* Sets object sprite to next image in the fireSprites array 
	 * */
	void setSprite() { 
		delay++; 
		if (imgNum == 6) { 
			imgNum = 0; 
		} 
		if (delay % 4 == 0) {
			spriteRender.sprite = fireSprites [imgNum]; 
			imgNum++; 
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

	/*Inflicts damage on enemey depending on how big the fireball is. 
	 * Max: 37 damage, Min: 5 damage 
	 * If fireball is in the range minimum - small: damage = 10 * size of fireball (minimum of 5 damage)
	 * medium -> large = 15 * size of fireball 
	 * large -> max size = 25 * size of fireball (maximum of 37 damage) */ 
	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Enemy") { 
			GameObject enemy = collision.gameObject; 
			eStats = enemy.GetComponent<EnemyStats> (); 
			float damage = getDamageMultiplier() * size; 
			eStats.damage ((int)damage, "FIRE"); 
			Destroy (gameObject); 
		} 
	}

}  
