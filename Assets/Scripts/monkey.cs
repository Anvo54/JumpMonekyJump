using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour {

    public GameObject target;
    public GameObject nearestGrappingPoint;

    public float speed;

    private float startTime;

    public List<GameObject> grappingPointList;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FindNearestGrappingPoint();

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        if (Vector3.Distance(transform.position, target.transform.position) < 1) Destroy(target);

    }



    public void AddNewGrappingPoints(List<GameObject> newGrabbingpoints)
    {
        foreach (GameObject grappingPoint in newGrabbingpoints)
        {
            grappingPointList.Add(grappingPoint);
        }

    }

    void FindNearestGrappingPoint()
    {
        GameObject[] grappingPoints = GameObject.FindGameObjectsWithTag("grabbingpoint");
        float nearesetDist = 1000;
        
        foreach (GameObject grappingPoint in grappingPoints)
        {
            if (Vector3.Distance(grappingPoint.transform.position, transform.position) < nearesetDist)
            {
                nearestGrappingPoint = grappingPoint;
                nearesetDist = Vector3.Distance(grappingPoint.transform.position, transform.position);
            }
        }
        target = nearestGrappingPoint;
        print("target set");
    }
}
