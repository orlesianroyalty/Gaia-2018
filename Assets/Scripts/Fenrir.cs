using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fenrir : MonoBehaviour {

	public int health;
	public bool forest;
	public bool desert;
	public bool arctic;
	public float damageRecieved;
	public int waitTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (switchTime ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void switchForm() {
		Random.seed = System.DateTime.Now.Millisecond;
		float rand = Random.Range (0, 30);
		Debug.Log ("rand called" + rand);
		if (rand <= 9 && forest != true) {
			//switch to forest
			forest = true;
			desert = false;
			arctic = false;
		}
		if (rand > 9 && rand <= 20 && desert != true) {
			//switch to fire
			desert = true;
			forest = false;
			arctic = false;
		}
		if (rand > 20 && rand <= 30 && arctic != true) {
			//switch to arctic
			arctic = true;
			forest = false;
			desert = false;
		} else {
			switchForm ();
		}
	}

	IEnumerator switchTime() {
		switchForm ();
		yield return new WaitForSeconds(400f);
	}



}
