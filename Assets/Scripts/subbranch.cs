using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subbranch : MonoBehaviour {

	public GameObject[] bushes;
	public GameObject bushLocationEmpty;

	private GameObject myBush;
	// Use this for initialization
	void Start () {
		float randomNumber = Random.value * 100;
		if (randomNumber < 50)
			myBush = Instantiate (bushes [0], bushLocationEmpty.transform.position, Quaternion.identity);
		else if ((randomNumber >= 50) && (randomNumber < 75)) 
			myBush = Instantiate (bushes [1], bushLocationEmpty.transform.position, Quaternion.identity);
		else if ((randomNumber >= 75) && (randomNumber < 95)) 
			myBush = Instantiate (bushes [2], bushLocationEmpty.transform.position, Quaternion.identity);
		else 
			myBush = Instantiate (bushes [3], bushLocationEmpty.transform.position, Quaternion.identity);
		myBush.transform.SetParent (transform);
	}	
	
	// Update is called once per frame
	void Update () {
		
	}
}
