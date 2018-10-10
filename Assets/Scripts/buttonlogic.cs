using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonlogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReplayButton(){
		SceneManager.LoadScene("SampleScene");
	}

	public void HomeButton(){
		SceneManager.LoadScene("MenuScene");
	}

	public void HighScore(){
		SceneManager.LoadScene("MenuScene");
	}
}
