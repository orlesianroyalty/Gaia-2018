using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Assign to player, contains script for RazorLeaf, Fireball, Sandstorm, and FrostBreath abilities. 
 * make sure to assign the appropriate script: ProjectileRazorLeaf / ProjectileFireball / ProjectileSandstorm / ProjectileFrostBreath 
 * to the ability gameObjects you assign to this script*/

public class Abilities : MonoBehaviour {

	private Camera cam; 
	private PlayerStats pStats; 

	/*------------------------------------razorleaf ability-------------------------------	*/
	public KeyCode leafKey = KeyCode.R;
	private Quaternion rotation; 
	public GameObject razorLeaf; 
	//private int leafStamina =
	//for cooldown
	private int leafDelay = 0; 

	/*------------------------------------fireball ability------------------------------- */
	public KeyCode fireKey = KeyCode.T;  
	public GameObject fireball;
	private Vector3 view, position;  
	private GameObject editedFireball; 
	//private int fireStamina = 
	//for cooldown
	private int fireDelay = 0; 

	/*------------------------------------sandstorm ability------------------------------- */
	public KeyCode sandKey = KeyCode.G; 
	private int sandstormStamina = 40; 
	public GameObject sand; 
	private GameObject sand1; 
	public float sandstormOrigin; 
	//for cooldown timer
	private int sandDelay = 0; 

	/*------------------------------ frost breath ability -------------------------------- */
	public KeyCode frostKey = KeyCode.F;  
	private int frostStamina = 2; 
	public GameObject frost; 
	//for cooldown timer
	private int frostDelay = 0;  

	/*----------------------------------------------------------------------------------  */

	void Start() { 
		pStats = gameObject.GetComponent<PlayerStats> (); 
	} 

	//every frame 
	void Update () {
		makeRazorLeaf (); 
		makeFireball (); 
		makeSandstorm (); 
		makeFrostBreath (); 
	}
		
	//(need to add useStamina)
	/* Launches a razor leaf depending when the user presses the 
	 * appropriate key. 'ProjectileRazorLeaf' script must be a component of 
	 * 'razorleaf' object for this to work */
	public void makeRazorLeaf() { 
		if (Input.GetKeyDown (leafKey) 
			&& leafDelay % 15 == 0) {  
				rotation = new Quaternion (0.0f, 0.0f, 45f, 0.0f); 
				Instantiate (razorLeaf, createLaunch(), rotation);
				leafDelay++; 
		} 
		if (leafDelay % 15 != 0) { 
		leafDelay++; 
		} 
	} 

	//(need to add useStamina)
	/* Launches a fireball depending on how long the user holds down 
	 * the trigger key. 'ProjectileFireball' script must be a component of 
	 * 'fireball' object for this to work. (need to add useStamina) */
	void makeFireball() { 
		if (Input.GetKeyDown (fireKey) 
			&& fireDelay % 25 == 0) {  
				editedFireball = Instantiate (fireball, createLaunch(), Quaternion.identity);
				editedFireball.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
				fireDelay++; 
		} 
		if (fireDelay % 25 != 0) { 
			fireDelay++; 
		} 
	} 


	/* Creates a sandstorm when  depending on how long the user holds down 
	 * the trigger key. 'ProjectileFireball' script must be a component of 
	 * 'fireball' object for this to work. */  
	void makeSandstorm() { 
		if (Input.GetKeyDown (sandKey) 
			&& pStats.useStamina(sandstormStamina)
			&& sandDelay % 25 == 0) { 
				sandstormOrigin = transform.position.x; 
				StartCoroutine (sandstormBTS ());
				sandDelay++; 
		} 
		if (sandDelay % 25 != 0) { 
			sandDelay++; 
		} 
	} 

	/* coroutine for sandstorm's 'sand' object generation */
	private IEnumerator sandstormBTS() {
		int sands = 0; 
		while (sands < 75) {  
			sands++; 
			Random.seed = System.DateTime.Now.Millisecond; 
			sand1 = Instantiate (sand, createSStormLaunch (), Quaternion.identity);
			sand1.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f); 
			yield return new WaitForSeconds (0.11f); 
			}
	}  

	/*frost breath */
	void makeFrostBreath() { 
		if (Input.GetKey (frostKey) && frostDelay % 8 == 0) { 
			Instantiate (frost, createFrostLaunch (), Quaternion.identity); 
		} 
		frostDelay++; 
	} 


	// creates launch point coordinates infront of character 
	public Vector3 createLaunch() {
		float playerX; 
		float playerY = gameObject.transform.position.y + 0.25f; 
		Vector3 launch;

		if (!facingLeft ()) { 
			playerX = gameObject.transform.position.x + 1.5f; 
			launch = new Vector3 (playerX, playerY,
			gameObject.transform.position.z);	
			return launch; 
		} 
		playerX = gameObject.transform.position.x - 1.5f; 
		launch = new Vector3 (playerX, playerY,   
		gameObject.transform.position.z);	
		return launch; 
	}


	// creates launch point coordinates around character for SandStorm
	public Vector3 createSStormLaunch() {
		float playerX; 
		float playerY = gameObject.transform.position.y + Random.Range (-0.5f, 5f); 
		Vector3 launch;

		if (!facingLeft ()) { 
			playerX = gameObject.transform.position.x + Random.Range(-5.0f, 5.0f); 
			launch = new Vector3 (playerX, playerY, gameObject.transform.position.z);	
			return launch; 
		} 
		playerX = gameObject.transform.position.x - Random.Range (-5.0f, 5.0f); 
		launch = new Vector3 (playerX, playerY, gameObject.transform.position.z);	
		return launch; 

	}

	// creates launch point coordinates infront of character for FrostBreath
	public Vector3 createFrostLaunch() {
		float playerX; 
		float playerY = gameObject.transform.position.y + 0.25f; 
		Vector3 launch;

		if (!facingLeft ()) { 
			playerX = gameObject.transform.position.x + 1f; 
			launch = new Vector3 (playerX, playerY,
				gameObject.transform.position.z);	
			return launch; 
		} 
		playerX = gameObject.transform.position.x - 1f; 
		launch = new Vector3 (playerX, playerY,   
			gameObject.transform.position.z);	
		return launch; 

	}
		
	/* returns:
	 * true if player is facing left 
	 * false if player is facing right 
	 * */
	bool facingLeft() { 
		if (gameObject.transform.localScale.x > 0) { 
			return false; 
		} 
		return true; 
	}
		
}
