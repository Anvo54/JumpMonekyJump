using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class branchSpawner : MonoBehaviour {

    public List<GameObject> branchSpawnpointList;
    public GameObject branch;

	// Use this for initialization
	void Start () {
        
        foreach (Transform child in transform)
        {
           
            if (child.gameObject.tag == "branchspawnpoint")
            {
                branchSpawnpointList.Add(child.gameObject);
                
            }
            
        }

        bool leftBranch = true;
        for (int i = 0; i< branchSpawnpointList.Count; i++)
        {
            GameObject newbranch = Instantiate(branch, branchSpawnpointList[i].transform.position, Quaternion.identity);

            if (leftBranch == true)
            {
                newbranch.transform.Rotate(0, 180, 0);
                leftBranch = false;
            }
            else leftBranch = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
