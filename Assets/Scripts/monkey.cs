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
	public GameObject musicPlayer;

    public List<GameObject> rightGrabbingPointList;
	public List<GameObject> leftGrabbingPointList;
	public Text bananaOMeterText;

	private Rigidbody2D myRB;
	private bool doubleTap;
	private bool lastTapRight;
	private int mouseClicks;
	private bool mouseClicksStarted;
	static readonly float MOUSE_TIMER_LIMIT = 0.25f;


    // Use this for initialization
    void Start () {
        bananaOMeter = 100;
		myRB = gameObject.GetComponent<Rigidbody2D> ();

    }

    // Update is called once per frame
    void Update () {
		if (mystate != "dead") {
			

			bananaOMeter -= Time.deltaTime * 10;
		//	Debug.Log (bananaOMeter);
			bananaOMeterText.text = Mathf.Round (bananaOMeter).ToString ();

			if (Input.GetMouseButtonDown (0)) {
				OnClick ();
				if (Input.mousePosition.x < Screen.width / 2) {
					JumpLeft ();
				} else if (Input.mousePosition.x > Screen.width / 2) {
					JumpRight ();
				}
				if (IsDoubleTap())
					doubleTap = true;
			}




			if (Input.GetKeyDown ("w")) {
				JumpLeft ();
			}
			if (Input.GetKeyDown ("e")) {
				JumpRight ();
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
				if (!grabbedBranch.broken) grabbedBranch.Break ();
				if (!doubleTap)	StartCoroutine ("CartoonDrop", grabbedBranch);
			}
			target.GetComponent<grabbingpoint> ().grabbable = false;
			target = null;
			RemoveUnderneathGrabbingPoints ();

			if (doubleTap == true) {
				if (lastTapRight)
					JumpRight ();
				else
					JumpLeft ();
				doubleTap = false;	
			}


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
			if ((grabbingPoint.transform.position.y < lowest) && (grabbingPoint.transform.position.y > transform.position.y) && (grabbingPoint.GetComponent<grabbingpoint>().grabbable))
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

	IEnumerator CartoonDrop(branch currentBranch){
		if (Vector3.Distance (transform.position, currentBranch.gameObject.transform.Find("grabbingpoint").transform.position) < 0.1f) {
			mystate = "dead";
			musicPlayer.GetComponent<AudioSource> ().enabled = false;
			yield return new WaitForSeconds (2f);
			myRB.isKinematic = false;
			myRB.AddForce (new Vector2 (0, -100), ForceMode2D.Impulse);
			doubleTap = false;
			StartCoroutine ("RestartDelay");
		}
			
	}

	public static bool IsDoubleTap(){
		bool result = false;
		float MaxTimeWait = 1;
		float VariancePosition = 1;

		if( Input.touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			float DeltaTime = Input.GetTouch (0).deltaTime;
			float DeltaPositionLenght=Input.GetTouch (0).deltaPosition.magnitude;

			if ( DeltaTime> 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
				result = true;                
		}
		Debug.Log (result);
		return result;
	}

	private void JumpLeft(){
		Debug.Log ("Vasen");
		FindNearestGrappingPointLeft ();
		target = nearestGrabbingPointLeft;
		lastTapRight = false;
	}

	private void JumpRight(){
		Debug.Log ("Oikea");
		FindNearestGrappingPointRight ();
		target = nearestGrabbingPointRight;
		lastTapRight = true;
	}

	public void OnClick(){
		mouseClicks++;
		if(mouseClicksStarted){
			return;
		}
		mouseClicksStarted = true;
		Invoke("checkMouseDoubleClick",MOUSE_TIMER_LIMIT);
	}


	private void checkMouseDoubleClick()
	{
		if(mouseClicks > 1){
			Debug.Log("Doubleclic");
			doubleTap = true;

		}else{
			Debug.Log("Singleclick");

		}
		mouseClicksStarted = false;
		mouseClicks = 0;
	}

	IEnumerator RestartDelay(){
		yield return new WaitForSeconds (3f);
		Application.LoadLevel(Application.loadedLevel);
	}
}
