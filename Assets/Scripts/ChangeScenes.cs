using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickDesert() {
		SceneManager.LoadScene ("Scenes/Desert");
	}

	public void OnClickArctic(){
		SceneManager.LoadScene ("Scenes/Arctic");
	}

	public void OnClickForest(){
		SceneManager.LoadScene ("Scenes/Forest");
	}
}
