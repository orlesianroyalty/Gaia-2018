﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShardIceScript : MonoBehaviour {

    private PlayerStats stats;

	// Use this for initialization
	void Start () {
		stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			stats.iceShardFound ();
			Destroy (gameObject);
		}
	}
}
