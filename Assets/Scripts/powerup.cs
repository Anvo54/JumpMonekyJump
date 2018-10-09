using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour {

    monkey monkeyInstance;
    // Use this for initialization
    void Start () {
        monkey monkeyInstance = GameObject.FindGameObjectWithTag("monkey").GetComponent<monkey>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
