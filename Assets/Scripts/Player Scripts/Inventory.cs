using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Collider> letterInventory;
    public bool keyInventory;



	// Use this for initialization
	void Start () {
        letterInventory = new List<Collider>();
        keyInventory = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Letter"))
        {
            letterInventory.Add(other);
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.CompareTag("Key"))
        {
            keyInventory = true;
            Destroy(other.gameObject);
        }
    }
}
