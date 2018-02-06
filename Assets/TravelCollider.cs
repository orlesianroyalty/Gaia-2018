using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelCollider : MonoBehaviour {

	public Canvas travelCanvas;

	// Use this for initialization
	void Start () {
		travelCanvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		travelCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		travelCanvas.enabled = true;
	}

	void OnTriggerExit() {
		travelCanvas.enabled = false;
	}
}
