using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squirrel : MonoBehaviour {

    public float speed;
    public GameObject nearestGrabbingPointRight;
    public GameObject nearestGrabbingPointLeft;

    public List<GameObject> rightGrabbingPointList;
    public List<GameObject> leftGrabbingPointList;


    private GameObject[] branches;

    private GameObject target;
    private Vector3 extractionPoint;

    // Use this for initialization
    void Start () {

        branches = GameObject.FindGameObjectsWithTag("branch");

        float highest = 1;
        foreach (GameObject branch in branches)
        {
            if (branch.transform.position.y > highest)
            {
                highest = branch.transform.position.y;
                extractionPoint = new Vector3(branch.transform.position.x + 20, highest - 2, 0);
            }
            foreach (Transform child in branch.transform)
            { //add grabbing points to squirrel
                if (child.gameObject.tag == "grabbingpoint")
                {
                    if (child.position.x > 0) rightGrabbingPointList.Add(child.gameObject);
                    else leftGrabbingPointList.Add(child.gameObject);
                }
                
            }
        }

        JumpRight();
    }

 

    // Update is called once per frame
    void Update () {
        if (transform.position.y < extractionPoint.y)
        {
            ClimbToTarget();
        }
        else
        {
            Extract();
        }
            
    }

    private void Extract()
    {
        float mydist = Vector3.Distance(transform.position, extractionPoint);
        // The step size is equal to speed times frame time.
        float step = (mydist / 20) + speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, extractionPoint, step);
    }

    void ClimbToTarget()
    {
        if (target == null)
            return;
        float mydist = Vector3.Distance(transform.position, target.transform.position);
        // The step size is equal to speed times frame time.
        float step = (mydist / 20) + speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        if (mydist < 0.1f)
        {
            target = null;
            float rnd = Random.value;
            if (rnd > 0.5f) JumpRight();
            else JumpLeft();


        }
    }

    private void JumpLeft()
    {
        FindNearestGrappingPointLeft();
        target = nearestGrabbingPointLeft;

    }

    private void JumpRight()
    {
        FindNearestGrappingPointRight();
        target = nearestGrabbingPointRight;

    }


    void FindNearestGrappingPointRight()
    {
        float lowest = 1000;

        foreach (GameObject grabbingPoint in rightGrabbingPointList)
        {
			if (grabbingPoint == null) {
				Extract ();
			} else {
				if ((grabbingPoint.transform.position.y < lowest) && (grabbingPoint.transform.position.y > transform.position.y) && (Vector3.Distance(grabbingPoint.transform.position,transform.position) > 0.5f))
				{
					nearestGrabbingPointRight = grabbingPoint;
					lowest = grabbingPoint.transform.position.y;
				}
			}

        }

    }

    void FindNearestGrappingPointLeft()
    {
        float lowest = 1000;

        foreach (GameObject grabbingPoint in leftGrabbingPointList)
        {
			if (grabbingPoint == null) {
				Extract ();
			} else {
				if ((grabbingPoint.transform.position.y < lowest) && (grabbingPoint.transform.position.y > transform.position.y) && (Vector3.Distance(grabbingPoint.transform.position, transform.position) > 0.5f))
				{
					nearestGrabbingPointLeft = grabbingPoint;
					lowest = grabbingPoint.transform.position.y;
				}
			}

        }

    }
}
