using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyDelay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
