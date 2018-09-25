using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackingBird : MonoBehaviour {
	public int moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-moveSpeed*Time.deltaTime,-moveSpeed*Time.deltaTime,0);
	}
}
