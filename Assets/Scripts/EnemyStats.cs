using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

	public int maxHp;
	public int hp;
	public float maxStamina;
	public float stamina;

	public bool stunned = false;
	public bool blinded = false;

	public int touchDamage = 15;

	public float criticalDamageFactor = 1.3f;
	public string elementalType = "NORMAL";

	public Slider healthbar;
	private Animator ani;

	void Start() {
		hp = maxHp;
		stamina = maxStamina;
		ani = GetComponent<Animator> ();
	}

	void Update() {
		GetComponent<EnemyController>().enabled = !stunned;
		Debug.Log (stunned);
	}

	public void damage(int dmg, string damageType) {
		if (damageType == "NORMAL") {
			hp -= dmg;
			//updateHealthUI ();
		}
		if (damageType == "FIRE" && elementalType == "ICE" 
			|| damageType == "ICE" && elementalType == "FIRE") {
			hp -= (int)(dmg * criticalDamageFactor);
			//updateHealthUI ();
		}
		if (hp <= 0) {
			Destroy (gameObject);
		}
	}

	public bool isStunned () {
		return stunned;
	}

	public void stun (float durationInSeconds) {
		Debug.Log ("stun called");
		StartCoroutine(stunTimer (durationInSeconds));
	}

	public void blind (float durationInSeconds) {
		blindTimer (durationInSeconds);
	}

	IEnumerator stunTimer(float stunDurationInSeconds) {
		stunned = true;
		yield return new WaitForSeconds (stunDurationInSeconds);
		stunned = false;
		Debug.Log ("Timer completed. Stunned should be false now. Actual state of stun: " + stunned);
	}

	IEnumerator blindTimer(float blindDurationInSeconds) {
		blinded = true;
		yield return new WaitForSeconds (blindDurationInSeconds);
		blinded = false;
		Debug.Log ("Timer completed. Blinded should be false now. Actual state of blinded: " + blinded);
	}

	void updateHealthUI () {
		healthbar.value = hp;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("FOUND YOU");
			if (!col.gameObject.GetComponent<PlayerMovement> ().isDashing) {
				col.gameObject.GetComponent<PlayerStats> ().damage (touchDamage, elementalType);
			} else {
				damage (col.gameObject.GetComponent<PlayerStats> ().baseDashDamage, col.gameObject.GetComponent<PlayerStats> ().getElementalType());
				ani.SetBool ("isHurt", true);
				hurtDisplayTime (1f);
			}
		}
	}

	IEnumerator hurtDisplayTime(float blindDurationInSeconds) {
		yield return new WaitForSeconds (blindDurationInSeconds);
		ani.SetBool ("isHurt", false);
	}
}
