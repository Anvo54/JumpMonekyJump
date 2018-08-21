using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeSpawner : MonoBehaviour {

    public GameObject tree;
    public GameObject mycamera;
    public GameObject mymonkey;

    private GameObject newtree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mycamera.transform.position.y - transform.position.y > 14) CreateNewTree();
    }

    void CreateNewTree()
    {
        newtree = Instantiate(tree, transform.position, Quaternion.identity);
        transform.Translate(0, 14, 0);
        FeedGrappingPoints();
    }

    void FeedGrappingPoints()
    {
        List<GameObject> grappingPoints = new List<GameObject>();
        foreach (Transform child in newtree.transform)
        {

            if (child.gameObject.tag == "branch")
            {
                grappingPoints.Add(child.gameObject);

            }

        }

        monkey monkeyinstance = mymonkey.GetComponent<monkey>();
        monkeyinstance.AddNewGrappingPoints(grappingPoints);
    }
}
