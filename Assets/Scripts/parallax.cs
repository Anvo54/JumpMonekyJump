using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {
    [SerializeField]
    private float parallaxSpeed;
	private string monkeyState;
	[SerializeField]
	GameObject monkey;
	[SerializeField]
    private float fadeOutTime;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		monkeyState = monkey.GetComponent<monkey> ().mystate;

		if(monkeyState != "dead"){
			transform.Translate(0, parallaxSpeed* Time.deltaTime, 0);

		} else GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, fadeOutTime));

        
	}
}
