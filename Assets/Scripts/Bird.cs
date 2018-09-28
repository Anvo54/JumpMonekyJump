using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {


    public float speed;

    public float frequency;  // Speed of sine movement
    public float magnitude;   // Size of sine movement
    private Vector3 axis;
    private Vector3 pos;

    GameObject mymonkey;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        axis = transform.up;  // May or may not be the axis you want

        mymonkey = GameObject.FindGameObjectWithTag("monkey");
        StartCoroutine("DeathDelay");
        
		
	}
	
	// Update is called once per frame
	void Update () {

            pos += -transform.right * Time.deltaTime * speed;
            transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;

            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

	
	}
