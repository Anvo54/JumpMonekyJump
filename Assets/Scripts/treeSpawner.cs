using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treeSpawner : MonoBehaviour {

    public GameObject tree;
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
	public GameObject shroom;
    public GameObject bird;
    public GameObject attackBird;
    public GameObject squirrel;
    public float bananaProbability;
	public float shroomProbability;
    public float birdProbability;
    public float attackbirdProbability;
    public float squirrelProbability;
    public float badbranchProbability;
	public float nobranchProbability;
    public float climbHeight;
    public Text reached;
    public float reachTextTime = 0.7f;

    private Score myscore;
    // Use this for initialization
    void Start () {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
		monkeyinstance = mymonkey.GetComponent<monkey>();
		CreateNewTree ();
        myscore = gameObject.GetComponent<Score>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y - myCamera.transform.position.y < 25) CreateNewTree();
        climbHeight = monkeyinstance.gameObject.transform.position.y;
        Debug.Log(reachTextTime);
        //  Debug.Log(climbHeight);
        if (monkeyinstance.mystate != "dead") myscore.MyScore = (int) climbHeight;
    }

    void CreateNewTree()
    {
        newtree = Instantiate(tree, transform.position, Quaternion.identity);
        transform.Translate(0, 20, 0);
		CreateBranches ();

        float randomNumber;

        if (climbHeight < 50)
        {
            Debug.Log("below 50");
            randomNumber = Random.value * 100;
            if (randomNumber< birdProbability)
            {
                SpawnBird(newtree);
            }

        }
        else if(climbHeight > 50 && climbHeight < 100)
        {
            if(reachTextTime > 0)
            {
                reached.text = "100m Reached!!";
                reachTextTime -= Time.deltaTime;
                
            }
            else
            {
                reached.text = "";
            }
            
            myCamera.GetComponent<Camera>().backgroundColor = new Color(0, 0, 0);
            Debug.Log("Reached 100m");
			badbranchProbability = 20;
            bananaProbability -= 5;
            randomNumber = Random.value * 100;
            if (randomNumber < birdProbability)
            {
                SpawnBird(newtree);

            }

            randomNumber = Random.value * 100;
            if(randomNumber < squirrelProbability)
            {
                SpawnSquirrel(newtree);
            }
        }
        else if (climbHeight > 150)
        {
            Debug.Log("150m Reached!");
			badbranchProbability = 30;
            bananaProbability -= 10;
            randomNumber = Random.value * 100;
            if (randomNumber < birdProbability)
            {
                SpawnBird(newtree);
            }
            randomNumber = Random.value * 100;
            if ( randomNumber < attackbirdProbability)
            {
                SpawnAttackBird(newtree);
            }
            randomNumber = Random.value * 100;
            if ( randomNumber < squirrelProbability)
            {
                SpawnSquirrel(newtree);
            } 
        }

        /*if (randomNumber < birdProbability) {

            SpawnBird(newtree);
            //SpawnAttackBird(newtree);
            //Squirrel(newtree);
        }*/
        
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

			if (BranchIsBad())
				continue;
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
			if (randomNumber < bananaProbability)
				SpawnBanana (newBranchGrabbingPoint);
			else {
				randomNumber = Random.value * 100;
				if (randomNumber < shroomProbability)
					SpawnShroom (newBranchGrabbingPoint);
			}

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

	bool BranchIsNot()
	{
		float randomNumber = Random.value * 100;
		if (randomNumber < nobranchProbability) return true;
		else return false;
	}

    void SpawnBanana(GameObject bananaBranch)
    {
        Instantiate(banana, bananaBranch.transform.position, Quaternion.identity);
    }

	void SpawnShroom(GameObject shroomBranch)
	{
		Instantiate(shroom, shroomBranch.transform.position, Quaternion.identity);
	}

    void SpawnBird(GameObject birdTree)
    {
        float randomYOffset = Random.value * 10;
        Vector3 birdPosition = new Vector3(birdTree.transform.position.x + 20, myCamera.transform.position.y +5 + randomYOffset, 0);
        Instantiate(bird, birdPosition, Quaternion.identity);
    }

        void SpawnAttackBird(GameObject attackBirdTree)
    {
        float randomYOffset = Random.Range(5,+8);
        Vector3 birdPosition = new Vector3(attackBirdTree.transform.position.x + 6, myCamera.transform.position.y +5 + randomYOffset, 0);
        Instantiate(attackBird, birdPosition, Quaternion.identity);
    }

    void SpawnSquirrel(GameObject squirrelTree)
    {
        float randomYOffset = Random.Range(5, +8);
		Vector3 squirrelPosition = new Vector3(squirrelTree.transform.position.x + 6, monkeyinstance.transform.position.y - 10, 0);
        Instantiate(squirrel, squirrelPosition, Quaternion.identity);
   //     squirrel.transform.Translate(0, -10, 0);
    }

}
