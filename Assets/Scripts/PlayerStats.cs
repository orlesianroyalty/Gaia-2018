using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

	public int maxHp;
	public int hp;
	public float maxStamina;
	public float stamina;

    public int grassShardsCollected;
    public int fireShardsCollected;
    public int iceShardsCollected;

	public bool stunned = false;

	public float criticalDamageFactor = 1.3f;
	public string elementalType = "NORMAL";

	private Slider healthbar;
	private Slider staminabar;

	public bool foundGrassJem = false;
	public bool foundFireJem = false;
	public bool foundIceJem = false;

	public int baseDashDamage = 25;

	string scenename;

	void Start() {
		hp = maxHp;
		stamina = maxStamina;
		healthbar = GameObject.Find ("Health Bar").GetComponent<Slider> ();
		staminabar = GameObject.Find ("Stamina Bar").GetComponent<Slider> ();
		InvokeRepeating ("regenTimer", 0f, .5f);
		updateStaminaUI ();
		scenename = SceneManager.GetActiveScene().name;

        grassShardsCollected = 0;
        fireShardsCollected = 0;
        iceShardsCollected = 0;
	}

	void Update() {
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
		if (hp <= 0) {
			Destroy (gameObject);
		}
	}

	public void damage(int dmg, string damageType) {
		if (damageType == "NORMAL") {
			hp -= dmg;
			updateHealthUI ();
		}
		if (damageType == "FIRE" && elementalType == "ICE" 
			|| damageType == "ICE" && elementalType == "FIRE") {
			hp -= (int)(dmg * criticalDamageFactor);
			updateHealthUI ();
		}
	}

	public bool isStunned () {
		return stunned;
	}

	public void stun (float durationInSeconds) {
		stunTimer (durationInSeconds);
	}

	IEnumerator stunTimer(float stunDurationInSeconds) {
		stunned = true;
		yield return new WaitForSeconds (stunDurationInSeconds);
		stunned = false;
		Debug.Log ("Timer completed. Stunned should be false now. Actual state of stun: " + stunned);
	}

	void regenTimer() {
		if (inAppropriateBiome()) {
			if (stamina < maxStamina) {
				stamina += 2;
			} else if (stamina > maxStamina) {
				stamina = maxStamina;
			}
			if (hp < maxHp) {
				hp += 1;
			}
		} else {
			if (stamina > 0) {
				stamina -= 2;
			} else if (stamina < 0) {
				stamina = 0;
			}
			hp -= 1;
		}
		updateStaminaUI ();
		updateHealthUI ();
	}

	public bool useStamina(int stam) {
		if (stamina >= stam) {
			stamina -= stam;
			return true;
		}
		return false;
	}

	void updateHealthUI () {
		healthbar.value = hp;
	}

	void updateStaminaUI() {
		staminabar.value = stamina;
	}

	private bool inAppropriateBiome() {
		if (scenename == "Arctic" && elementalType == "Fire") {
			return false;
		}
		if (scenename == "Desert" && elementalType == "Ice") {
			return false;
		}
		return true;
	}

    public void grassShardFound() {
        grassShardsCollected++;
        unlockGrassJem();
    }

    public void fireShardFound()
    {
        fireShardsCollected++;
        unlockFireJem();
    }

    public void iceShardFound()
    {
        iceShardsCollected++;
        unlockIceJem();
    }
    public void unlockGrassJem() {
        if (grassShardsCollected >= 50) {
            foundGrassJem = true;
            GameObject.Find("GreenShardDisplay").GetComponent<Image>().enabled = true;
        }
		
	}

	public void unlockFireJem() {
        if (fireShardsCollected >= 75)
        {
            foundFireJem = true;
            GameObject.Find("RedShardDisplay").GetComponent<Image>().enabled = true;
        }
	}

	public void unlockIceJem() {
        if (iceShardsCollected >= 100)
		foundIceJem = true;
		GameObject.Find ("IceShardDisplay").GetComponent<Image> ().enabled = true;
	}

	public void changeElementalTypeToGrass () {
		if (foundGrassJem) {
			elementalType = "GRASS";
//			Sprite spr = Resources.Load<Sprite> ("ArcticShoe");
//			SpriteRenderer rend = GameObject.Find ("Torso").GetComponent<SpriteRenderer> ();
//			rend.sprite = spr;
//			Debug.Log (spr);
		}
	}

	public void changeElementalTypeToFire () {
		if (foundFireJem) {
			elementalType = "FIRE";
			//Sprite spr = Resources.Load<Sprite> ("ArcticShoe");
//			Sprite spr = Resources.Load("ArcticShoe", typeof(Sprite)) as Sprite;
//			SpriteRenderer rend = GameObject.Find ("Right Shoe").GetComponent<SpriteRenderer> ();
//			rend.sprite = spr;
//			Debug.Log (spr);

		}
	}

	public void changeElementalTypeToIce () {
		if (foundIceJem) {
			elementalType = "ICE";
//			Sprite spr = Resources.Load<Sprite> ("ArcticShoe");
//			SpriteRenderer rend = GameObject.Find ("Torso").GetComponent<SpriteRenderer> ();
//			rend.sprite = spr;
//			Debug.Log (spr);
		}
	}

	public string getElementalType() {
		return elementalType;
	}
}
