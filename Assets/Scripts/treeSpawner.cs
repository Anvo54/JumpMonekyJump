using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeSpawner : MonoBehaviour {

    public GameObject tree;
    public GameObject mycamera;
    public GameObject mymonkey;
	private monkey monkeyinstance;

    private GameObject newtree;
	public List<GameObject> branchSpawnpointList;
	public GameObject branch;
	// Use this for initialization
	void Start () {
		monkeyinstance = mymonkey.GetComponent<monkey>();
		CreateNewTree ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y - mycamera.transform.position.y < 20) CreateNewTree();
    }

    void CreateNewTree()
    {
        newtree = Instantiate(tree, transform.position, Quaternion.identity);
        transform.Translate(0, 20, 0);
		CreateBranches ();
    }

	void CreateBranches()
	{
		foreach (Transform child in newtree.transform)
		{

			if (child.gameObject.tag == "branchspawnpoint")
			{
				branchSpawnpointList.Add(child.gameObject);

			}

		}

		bool nextBranchIsLeft = true;
		for (int i = 0; i< branchSpawnpointList.Count; i++)
		{
			if (nextBranchIsLeft == true) { //create branch to the left
				GameObject newBranch = Instantiate (branch, branchSpawnpointList [i].transform.position, Quaternion.identity);
				branch newBranchInstance = newBranch.GetComponent<branch> ();
				newBranchInstance.left = true;
				newBranch.transform.Rotate (0, 180, 0);
				nextBranchIsLeft = false; //reset leftbranch boolean so the next branch will be right

				foreach (Transform child in newBranch.transform) { //add grabbing point to the left hand side list of grappingpoints for the monkey
					if (child.gameObject.tag == "grabbingpoint") monkeyinstance.AddLeftGrappingPoint (child.gameObject);
				}

			} else { //create a branch to the right
				GameObject newBranch = Instantiate (branch, branchSpawnpointList [i].transform.position, Quaternion.identity);
				branch newBranchInstance = newBranch.GetComponent<branch> ();
				newBranchInstance.left = false;
				nextBranchIsLeft = true; //reset leftbranch boolean so the next branch will be left

				foreach (Transform child in newBranch.transform) { //add grabbing point to the right hand side list of grappingpoints for the monkey
					if (child.gameObject.tag == "grabbingpoint") monkeyinstance.AddRightGrappingPoint (child.gameObject);
				}
			}
		}

	}

}
