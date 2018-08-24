using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour {

    public GameObject target;
    public GameObject nearestGrabbingPointRight;
	public GameObject nearestGrabbingPointLeft;
    public float speed;
    public float bananaOMeter;

    public List<GameObject> rightGrabbingPointList;
	public List<GameObject> leftGrabbingPointList;

    // Use this for initialization
    void Start () {
        bananaOMeter = 100;


    }

    // Update is called once per frame
    void Update () {


        bananaOMeter -= Time.deltaTime*6;
        Debug.Log(bananaOMeter);

        if (Input.GetMouseButtonDown(0))
        {

            if (Input.mousePosition.x < Screen.width / 2)
            {
                Debug.Log("Vasen");
                FindNearestGrappingPointLeft();
                target = nearestGrabbingPointLeft;
            } else if (Input.mousePosition.x > Screen.width / 2)
            {
                Debug.Log("Oikea");
                FindNearestGrappingPointRight();
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

    


	void ClimbToTarget(){
		if (target == null)
			return;
		// The step size is equal to speed times frame time.
		float step = speed * Time.deltaTime;

		// Move our position a step closer to the target.
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

		if (Vector3.Distance (transform.position, target.transform.position) < 0.1f) {
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
            bananaOMeter +=10;
            Debug.Log("Banana Collected");
        }

    }

}
