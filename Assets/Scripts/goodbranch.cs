using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodbranch : MonoBehaviour {

    private SpriteRenderer spriter;
    // Use this for initialization
    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();   
        StartCoroutine("DestroyDelay");
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monkey")
        {
            Debug.Log("Oksa katkesi");
        }
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(25f);
        spriter.enabled = !spriter.enabled;
        Debug.Log("Oksa katki");
        
    }

}
