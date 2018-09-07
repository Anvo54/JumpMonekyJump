using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class monkey : MonoBehaviour {

	public string mystate;
    public GameObject target;
    public GameObject nearestGrabbingPointRight;
	public GameObject nearestGrabbingPointLeft;
    public float speed;
    public float bananaOMeter;

    public List<GameObject> rightGrabbingPointList;
	public List<GameObject> leftGrabbingPointList;
	public Text bananaOMeterText;

	private Rigidbody2D myRB;


    // Use this for initialization
    void Start () {
        bananaOMeter = 100;
		myRB = gameObject.GetComponent<Rigidbody2D> ();

    }

    // Update is called once per frame
    void Update () {
		if (mystate != "dead") {
			

			bananaOMeter -= Time.deltaTime * 10;

			Debug.Log (bananaOMeter);

			bananaOMeterText.text = Mathf.Round (bananaOMeter).ToString ();

			if (Input.GetMouseButtonDown (0)) {

				if (Input.mousePosition.x < Screen.width / 2) {
					Debug.Log ("Vasen");
					FindNearestGrappingPointLeft ();
					target = nearestGrabbingPointLeft;
				} else if (Input.mousePosition.x > Screen.width / 2) {
					Debug.Log ("Oikea");
					FindNearestGrappingPointRight ();
					target = nearestGrabbingPointRight;
				}
			}




			if (Input.GetKeyDown ("w")) {
				FindNearestGrappingPointLeft ();
				target = nearestGrabbingPointLeft;
			}
			if (Input.GetKeyDown ("e")) {
				FindNearestGrappingPointRight ();
				target = nearestGrabbingPointRight;
			}

			ClimbToTarget ();
		}
    }

    


	void ClimbToTarget(){
		if (target == null)
			return;
		float mydist = Vector3.Distance (transform.position, target.transform.position);
		bananaOMeter -= mydist/2;
		// The step size is equal to speed times frame time.
		float step = (mydist /20) + speed * Time.deltaTime;

		// Move our position a step closer to the target.
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

		if (mydist < 0.1f) {
			branch grabbedBranch = target.transform.parent.gameObject.GetComponent<branch> ();
			if (grabbedBranch.bad == true) {
				StartCoroutine ("CartoonDrop");
			}
				
			target.transform.Translate (0, -2, 0); //quick hack to prevent monkey getting the same grabbingpoint again
			target = null;
			RemoveUnderneathGrabbingPoints ();
		}
	}



    public void AddRightGrappingPoint(GameObject newGrabbingPoint)
    {
		rightGrabbingPointList.Add (newGrabbingPoint);
    }

	public void AddLeftGrappingPoint(GameObject newGrabbingPoint)
	{
		leftGrabbingPointList.Add (newGrabbingPoint);
	}

    void FindNearestGrappingPointRight()
    {
        float lowest = 1000;
        
		foreach (GameObject grabbingPoint in rightGrabbingPointList)
        {
			if ((grabbingPoint.transform.position.y < lowest) && (grabbingPoint.transform.position.y > transform.position.y))
			{
				nearestGrabbingPointRight = grabbingPoint;
				lowest = grabbingPoint.transform.position.y;
			}
        }

    }

	void FindNearestGrappingPointLeft()
	{
		float lowest = 1000;

		foreach (GameObject grabbingPoint in leftGrabbingPointList)
		{
			if ((grabbingPoint.transform.position.y < lowest) && (grabbingPoint.transform.position.y > transform.position.y))
			{
				nearestGrabbingPointLeft = grabbingPoint;
				lowest = grabbingPoint.transform.position.y;
			}
		}

	}

	void RemoveUnderneathGrabbingPoints()
	{
		for (int i = 0; i < rightGrabbingPointList.Count; i++) {
			if (rightGrabbingPointList [i].transform.position.y < transform.position.y) {
				rightGrabbingPointList.Remove (rightGrabbingPointList [i]);
			}
		}
		for (int i = 0; i < leftGrabbingPointList.Count; i++) {
			if (leftGrabbingPointList [i].transform.position.y < transform.position.y) {
				leftGrabbingPointList.Remove (leftGrabbingPointList [i]);
			}
		}
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "banana")
        {
            Destroy(collision.gameObject);
            bananaOMeter +=60;
            Debug.Log("Banana Collected");
        }

    }

	IEnumerator CartoonDrop(){
		mystate = "dead";
		yield return new WaitForSeconds (2f);
		myRB.isKinematic = false;
		myRB.AddForce (new Vector2 (0, -100), ForceMode2D.Impulse);
	}

}
