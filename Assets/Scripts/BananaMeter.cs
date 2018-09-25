using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaMeter : MonoBehaviour {


    public GameObject banana;

    public float bananaPower;
    public int maxBanana = 100;
    private int loseBanana = 5;

    public Image bananaMeter;


	// Use this for initialization
	void Start ()
    {
        bananaMeter = GetComponent<Image>();
        bananaPower = 100.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {

        bananaMeter.fillAmount = bananaPower / maxBanana;
        if (bananaPower >= 0)
        {
            bananaPower -= Time.deltaTime * loseBanana;
        }

	}

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("monkey"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        //bananaMeter.fillAmount = Mathf.Lerp()
    }*/
}
