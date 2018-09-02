using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float smoothTime;
	private Vector3 velocity = Vector3.zero;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {	
	Vector3 pPos = cam.WorldToScreenPoint(target.position);
	if (pPos.y>Screen.height/2){
		CameraFollower();
	} else{
		CameraMover();		
	}
	




	}
	void CameraMover(){
		transform.Translate(0, 4 * Time.deltaTime, 0);
	}
	void CameraFollower(){
		Vector3 newPos = new Vector3(0, target.position.y, -10);
		transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
		transform.Translate(0, 4 * Time.deltaTime, 0);
	}
}
