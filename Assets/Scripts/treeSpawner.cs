using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeSpawner : MonoBehaviour {

    public GameObject tree;
    public GameObject mycamera;
    public GameObject mymonkey;
    public GameObject myCamera;
	private monkey monkeyinstance;

    private GameObject newtree;
	public List<GameObject> branchSpawnpointList;
	public GameObject branch;
	public GameObject subbranch;
    public GameObject badbranch;
    public GameObject badsubbranch;
    public GameObject banana;
    public GameObject bird;
    public float bananaProbability;
    public float birdProbability;
    public float badbranchProbability;
    // Use this for initialization
    void Start () {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
		monkeyinstance = mymonkey.GetComponent<monkey>();
		CreateNewTree ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y - mycamera.transform.position.y < 25) CreateNewTree();
    }

    void CreateNewTree()
    {
        newtree = Instantiate(tree, transform.position, Quaternion.identity);
        transform.Translate(0, 20, 0);
		CreateBranches ();

        float randomNumber = Random.value * 100;
        if (randomNumber < birdProbability) SpawnBird(newtree);
        branchSpawnpointList.Clear();
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
            //create a new branch
            GameObject newBranch;
            GameObject newSubBranch;


            //is it bad brach?
            if (!BranchIsBad())
                newBranch = Instantiate (branch, branchSpawnpointList [i].transform.position, Quaternion.identity); 
            else
                newBranch = Instantiate(badbranch, branchSpawnpointList[i].transform.position, Quaternion.identity);
            branch newBranchInstance = newBranch.GetComponent<branch> ();
            
			if (nextBranchIsLeft == true) { //will the branch go left
				newBranchInstance.left = true; 
				//create a sub branch also to the left
                if (!newBranchInstance.bad) newSubBranch = Instantiate(subbranch, new Vector3(newBranch.transform.position.x  -2.3f, newBranch.transform.position.y, 0), Quaternion.identity);
                else newSubBranch = Instantiate(badsubbranch, new Vector3(newBranch.transform.position.x - 2.3f, newBranch.transform.position.y, 0), Quaternion.identity);
                newBranch.transform.localScale = new Vector3(-1,1,1);
				newSubBranch.transform.localScale = new Vector3 (-1, 1, 1);
				FixedJoint2D NBFJ = newBranch.GetComponent<FixedJoint2D> ();
				//NBFJ.anchor = new Vector3 (-2.32f, NBFJ.anchor.y);
				nextBranchIsLeft = false; //reset leftbranch boolean so the next branch will be right

				foreach (Transform child in newBranch.transform) { //add grabbing point to the left hand side list of grappingpoints for the monkey
					if (child.gameObject.tag == "grabbingpoint") monkeyinstance.AddLeftGrappingPoint (child.gameObject);
                }

			} else { //create a branch to the right
				newBranchInstance.left = false;
                if (!newBranchInstance.bad) newSubBranch = Instantiate(subbranch, new Vector3(newBranch.transform.position.x +2.3f , newBranch.transform.position.y, 0), Quaternion.identity);
                else newSubBranch = Instantiate(badsubbranch, new Vector3(newBranch.transform.position.x + 2.3f, newBranch.transform.position.y, 0), Quaternion.identity);

                nextBranchIsLeft = true; //reset leftbranch boolean so the next branch will be left
               
                foreach (Transform child in newBranch.transform) { //add grabbing point to the right hand side list of grappingpoints for the monkey
					if (child.gameObject.tag == "grabbingpoint") monkeyinstance.AddRightGrappingPoint (child.gameObject);

				}

            }
			//banana spawn
			float randomNumber = Random.value * 100;
			GameObject newBranchGrabbingPoint = newBranch.transform.Find("grabbingpoint").gameObject;
			if (randomNumber < bananaProbability) SpawnBanana(newBranchGrabbingPoint);

			//physics stuff, join rigidbodies with joints to get a bending effect
			FixedJoint2D newTreeJoint = branchSpawnpointList[i].GetComponent<FixedJoint2D>();
			Rigidbody2D newBranchRB = newBranch.GetComponent<Rigidbody2D>();
			Rigidbody2D newSubBranchRB = newSubBranch.GetComponent<Rigidbody2D>();
			newTreeJoint.connectedBody = newBranchRB;
			FixedJoint2D newBranchJoint = newBranch.GetComponent<FixedJoint2D> ();
			newBranchJoint.connectedBody = newSubBranchRB;

            
		}

        


	}

    bool BranchIsBad()
    {
        float randomNumber = Random.value * 100;
        if (randomNumber < badbranchProbability) return true;
        else return false;
    }

    void SpawnBanana(GameObject bananaBranch)
    {
        Instantiate(banana, bananaBranch.transform.position, Quaternion.identity);
    }

    void SpawnBird(GameObject birdTree)
    {
        float randomYOffset = Random.value * 10;
        Vector3 birdPosition = new Vector3(birdTree.transform.position.x + 20, myCamera.transform.position.y +5 + randomYOffset, 0);
        Instantiate(bird, birdPosition, Quaternion.identity);
    }

}
