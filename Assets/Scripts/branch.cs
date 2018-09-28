using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class branch : MonoBehaviour {

	public bool left;
    public bool bad;
    public bool broken;
    private Rigidbody2D myRB;
    private Rigidbody2D subBranchRB;
    private FixedJoint2D myJoint;
    private GameObject subBranch;
    public GameObject breakingParticles;
    public GameObject smokeParticles;
	// Use this for initialization
	void Start () {
        myRB = gameObject.GetComponent<Rigidbody2D>();
        myJoint = gameObject.GetComponent<FixedJoint2D>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Break()
    {
		broken = true;
        subBranch = myJoint.connectedBody.gameObject;
        subBranchRB = subBranch.GetComponent<Rigidbody2D>();
        myJoint.breakForce = 100;
        myJoint.breakTorque = 100;
        float randomForce = 20 + Random.value * 100;
		subBranchRB.AddTorque (20 + Random.value * 100);
        Vector2 randomDir = new Vector2(Random.value, Random.value);
        subBranchRB.AddForce(randomDir * randomForce, ForceMode2D.Impulse);
        GameObject newBreakingParticles = Instantiate(breakingParticles, subBranch.transform.position, Quaternion.identity);
        GameObject newSmokeParticles = Instantiate(smokeParticles, subBranch.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((bad == true) && (broken == false)) StartCoroutine("BreakDelay");
		if ((collision.gameObject.tag == "bird") || (collision.gameObject.tag == "attackBird") && (bad == true) && (broken == false))
			Break ();
    }

    IEnumerator BreakDelay()
    {
        broken = true;
        yield return new WaitForSeconds(0.1f);
        Break();
    }
}
